/*!@license
* Infragistics.Web.ClientUI Barcode localization resources 21.1.11
*
* Copyright (c) 2011-2021 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
(function(factory){if(typeof define==="function"&&define.amd){define(["jquery"],factory)}else{return factory(jQuery)}})(function($){$.ig=$.ig||{};$.ig.locale=$.ig.locale||{};$.ig.locale.ru=$.ig.locale.ru||{};$.ig.Barcode=$.ig.Barcode||{};$.ig.locale.ru.Barcode={aILength:"AI должен состоять из минимум 2-х цифр.",badFormedUCCValue:"Значение свойства Data в штрих-коде UCC задано некорректно. Допустимый формат: (AI)GTIN.",code39_NonNumericError:"Символ '{0}' недопустим в значении свойства Data для CODE39. Допустимые символы: {1}",countryError:"Ошибка при расшифровке кода в свойстве Country. Значение должно быть численным.",emptyValueMsg:"Значение свойства Data пусто.",encodingError:"Ошибка во время преобразования. Обратитесь к документации и проверьте правильность заданных свойств.",errorMessageText:"Недопустимое значение! Обратитесь к документации и проверьте правильность структуры свойства Data в штрих-коде.",gS1ExMaxAlphanumNumber:"Семейство GS1 DataBar Expanded способно закодировать до 41 буквенно-цифровых символов.",gS1ExMaxNumericNumber:"Семейство GS1 DataBar Expanded способно закодировать до 74 цифровых символов.",gS1Length:"Свойство Data в GS1 DataBar используется для GTIN - 8, 12, 13, 14 и его длинна должна быть 7, 11, 12 или 13. Последняя цифра зарезервирована для контрольной суммы.",gS1LimitedFirstChar:"Штрих-код GS1 DataBar Limited должен начинаться с 0 или 1. При кодировке данных GTIN-14 со значением Indicator больше 1, необходимо использовать Omnidirectional, Stacked, Stacked Omnidirectional или Truncated тип штрих-кода.",i25Length:"Штрих-код типа Interleaved2of5 должен иметь четное количество цифр. В случае нечетного количества цифр, можно добавить 0 впереди.",intelligentMailLength:"Длинна значения свойства Data для штрих-кода типа Intelligent Mail должна быть равна 20, 25, 29 или 31 символу: 20 цифр - это трэк-код (2 для идентификации штрих-кода, 3 для идентификации типа сервиса, 6 или 9 для идентификации отправителя, и 9 или 6 для серийного номера) и 0, 5, 9 или 11 символов почтового индекса.",intelligentMailSecondDigit:"Вторая цифра должна быть от 0 до 4.",invalidAI:"Недопустимое значение строки для Application Identifier. Пожалуйста убедитесь, что AI строка в свойстве Data задана корректно.",invalidCharacter:"Символ '{0}' недопустим для этого типа штрих-кода. Допустимые символы: {1}",invalidDimension:"Размерность штрих-кода не может быть определена из-за недопустимой комбинации значений для следующих свойств: Stretch, BarsFillMode и XDimension.",invalidHeight:"Такое количество рядов ({0}) разметки штрих-кода не может быть помещено в заданное количество пикселей по высоте: {1}.",invalidLength:"Значение свойства Data в штрих-коде должно иметь следующее количество цифр: {0}.",invalidPostalCode:"Недопустимое значение PostalCode - в режиме 2 кодируются до 9 цифр почтового индекса (почтовый индекс в США), а в режиме 3 кодируются до 6 символов буквенно-цифрового кода.",invalidPropertyValue:"Значение свойства {0} должно быть в диапазоне от {1} до {2}.",invalidVersion:"Число, заданное в свойстве SizeVersion, не позволяет сгенерировать достаточное количество клеток для кодировки данных в указанном режиме кодирования и уровня корректировки ошибок.",invalidWidth:"Такое количество колонок ({0}) разметки штрих-кода не может быть помещено в заданное количество пикселей по ширине: {1}. Проверьте значения свойств: XDimension и/или WidthToHeightRatio.",invalidXDimensionValue:"Для заданного типа штрих-кода, значение свойства XDimension должно быть в диапазоне от {0} до {1}.",maxLength:"Для заданного типа штрих-кода, длинна текста ({0}) превосходит максимально допустимую для кодировки. Максимально допустимое количество символов для кодировки: {1}.",notSupportedEncoding:"Кодировка соответствующая {0} {1} не поддерживается.",pDF417InvalidRowsColumnsCombination:"Кодовые слова (данные и корректировка ошибок) превышают допустимое количество, которое может быть закодировано в символьной матрице {0}x{1}.",primaryMessageError:"Невозможно извлечь основное сообщение из значения свойства Data. Обратитесь к документации для уточнения структуры этого значения.",serviceClassError:"Ошибка при преобразовании класса сервиса. Значение должно быть численным.",smallSize:"Невозможно уместить разметку в Size({0}, {1}) как указано в установках Stretch.",unencodableCharacter:"Символ '{0}' не может быть закодирован.",uPCEFirstDigit:"Первая цифра UPCE всегда должна быть нулем в соответствии со спецификацией.",warningString:"Штрих-код предупреждение: ",wrongCompactionMode:"Значение свойства Data не может быть упаковано в режиме {0}.",notLoadedEncoding:"Кодировка {0} не загружена."};$.ig.Barcode.locale=$.ig.Barcode.locale||$.ig.locale.ru.Barcode;return $.ig.locale.ru.Barcode});