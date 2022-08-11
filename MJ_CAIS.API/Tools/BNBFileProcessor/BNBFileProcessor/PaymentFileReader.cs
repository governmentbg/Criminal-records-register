using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using System;
using System.Globalization;
using System.IO;
using System.Text;


namespace BNBFileProcessor
{
    public class PaymentFileReader : IDisposable
    {
        private StreamReader Reader { get; }

        public int LastReadRow { get; private set; }

        public bool FileIsValid { get; private set; }

        public bool SkipPreviousProcessedRows { get; set; }

        public bool FileIsNewerThanLastProcessed { get; private set; }

        private readonly IPaymentColumn[] _columns = {
            new StringColumn(0, 1, (l, s) => l.EntryType = s),
            new StringColumn(2, 26, (l, s) => l.DestinationIban = s),
            new PaymentColumn<DateTime>(29, 8,
                (l, d) => l.PaymentDate = d,
                s => DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None)),
            new StringColumn(38, 6, (l, s) => l.DocumentNumber = s),
            new StringColumn(45, 1, (l, s) => l.WritingType = s),
            new PaymentColumn<decimal>(47, 20,
                (l, d) => l.SentAmount = d,
                s => decimal.Parse(s, CultureInfo.InvariantCulture)),
            new StringColumn(68, 26, (l, s) => l.CorrIban = s),
            new StringColumn(95, 6, (l, s) => l.CorrPaymentType = s),
            new StringColumn(106, 28, (l, s) => l.CorrReference = s),
            new StringColumn(135, 35, (l, s) => l.ContragentName = s),
            new StringColumn(171, 35, (l, s) => l.PaymentReason = s),
            new StringColumn(207, 35, (l, s) => l.PaymentReasonDetails = s),
            new StringColumn(243, 2, (l, s) => l.DocumentCode = s),
            new StringColumn(273, 1, (l, s) => l.AddInfoDocType = s),
            new StringColumn(247, 17, (l, s) => l.AddInfoDocNum = s),
            new StringColumn(264, 8, (l, s) => l.AddInfoDocDate = s),
            new StringColumn(272, 8, (l, s) => l.AddInfoPeriodFrom = s),
            new StringColumn(280, 8, (l, s) => l.AddInfoPeriodTo = s),
            new StringColumn(288, 30, (l, s) => l.AddInfoPersonName = s),
            new StringColumn(318, 13, (l, s) => l.AddInfoPersonBulstat = s),
            new StringColumn(331, 10, (l, s) => l.AddInfoPersonEgn = s),
            new StringColumn(341, 10, (l, s) => l.AddInfoPersonLnch = s),
        };

   
        public PaymentFileReader(string paymentFilePath, int encoding, string iban, bool skipLastProcessedRows = false)
        {
            LastReadRow = 0;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding1;
            try
            {
                encoding1 = Encoding.GetEncoding(encoding);
            }
            catch
            {
                encoding1 = Encoding.Default;
            }

            SkipPreviousProcessedRows = skipLastProcessedRows;
            ValidateFile(paymentFilePath, encoding1, iban);
            Reader = new StreamReader(paymentFilePath, encoding1);
            //todo: може би няма смисъл
            //await SkipProcessedRows(dbContext);

        }

        private void ValidateFile(string paymentFilePath, Encoding encoding, string bnbIban)
        {
          
            string firstRow;
            using (var peekReader = new StreamReader(paymentFilePath, encoding))
                firstRow = peekReader.ReadLine();
            if (firstRow != null)
                FileIsValid = firstRow.Length >= 28 && firstRow.Substring(2, 26).Trim() == bnbIban;
        }

        private async Task SkipProcessedRows(CaisDbContext dbContext)
        {
            var lastProcessedRow = (await dbContext.ESynchronizationParameters.FirstOrDefaultAsync(p => p.Name == SynchronizationConstants.SynchronizationParametersNames.BNB_LAST_PROCESSED_ROW))
               ?.LastId;
           
            if (!SkipPreviousProcessedRows || !lastProcessedRow.HasValue)
                return;
            FileIsNewerThanLastProcessed = false;
            while (!Reader.EndOfStream || FileIsNewerThanLastProcessed)
            {
                if (LastReadRow == lastProcessedRow)
                    FileIsNewerThanLastProcessed = true;
                else
                {
                    Reader.ReadLine();
                    LastReadRow++;
                }
            }
        }

        public bool EndOfFile => Reader.EndOfStream;

        public EBnbPayment ReadLine()
        {
            if (Reader.EndOfStream)
                return null;
            LastReadRow++;
            return ParseRow(Reader.ReadLine());
        }

        public void Dispose() => Reader?.Dispose();

        private EBnbPayment ParseRow(string row)
        {
            var log = new EBnbPayment();
            try
            {
                foreach (var column in _columns)
                    column.Parse(row, log);
                return log;
            }
            catch
            {
                return null;
            }
        }
    }

    public interface IPaymentColumn
    {
        void Parse(string row, EBnbPayment EBnbPayment);
    }

    public class PaymentColumn<T> : IPaymentColumn
    {
        private int StartIndex { get; }

        private int Length { get; }

        private Action<EBnbPayment, T> Setter { get; }

        private Func<string, T> Parser { get; }

        public PaymentColumn(int startIndex, int length, Action<EBnbPayment, T> setter, Func<string, T> parser)
        {
            StartIndex = startIndex;
            Length = length;
            Setter = setter;
            Parser = parser;
        }

        public void Parse(string row, EBnbPayment paymentLog) =>
            Setter(
                paymentLog,
                Parser(
                    row.Length < StartIndex + Length
                        ? string.Empty
                        : row.Substring(StartIndex, Length).Trim()));
    }

    public class StringColumn : PaymentColumn<string>
    {
        public StringColumn(int startIndex, int length, Action<EBnbPayment, string> setter)
            : base(startIndex, length, setter, s => s)
        {
        }
    }
}