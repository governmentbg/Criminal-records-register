using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoLogica.RegiX.NRAObligatedPersonsAdapter
{
    public partial class IdentityType
    {
        public bool ShouldSerializeID()
        {
            bool haveSomeValue = !string.IsNullOrEmpty(this.ID);

            if (!haveSomeValue)
            {
                this.ID = null;
            }

            return haveSomeValue;
        }
    }
    public partial class ResponseIdentityType
    {
        public bool ShouldSerializeID()
        {
            bool haveSomeValue = !string.IsNullOrEmpty(this.ID);

            if (!haveSomeValue)
            {
                this.ID = null;
            }

            return haveSomeValue;
        }
    }
    public partial class StatusType
    {
        public bool ShouldSerializeMessage()
        {
            bool haveSomeValue = !string.IsNullOrEmpty(this.Message);

            if (!haveSomeValue)
            {
                this.Message = null;
            }

            return haveSomeValue;
        }
    }
    public partial class ObligationResponse
    {
        public bool ShouldSerializeIdentity()
        {
            bool haveSomeValue = this.Identity != null &&
                (
                this.Identity.TYPESpecified != default(Boolean) ||
                this.Identity.ShouldSerializeID()
                );

            if (!haveSomeValue)
            {
                this.Identity = null;
            }

            return haveSomeValue;
        }

        public bool ShouldSerializeStatus()
        {
            bool haveSomeValue = this.Status != null &&
                (
                this.Status.Code != default(Int32) ||
                this.Status.CodeSpecified != default(Boolean) ||
                this.Status.ShouldSerializeMessage()
                );

            if (!haveSomeValue)
            {
                this.Status = null;
            }

            return haveSomeValue;
        }
    }
}
