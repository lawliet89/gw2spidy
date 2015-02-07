using System.Text;

namespace Gw2spidyApi.Objects
{
    public class Currency : Gw2Object
    {
        private readonly int raw;

        public Currency(int raw)
        {
            this.raw = raw;
        }

        public int Gold
        {
            get
            {
                if (raw < 10000) return 0;
                return raw/10000;
            }
        }

        public int Silver
        {
            get
            {
                if (raw < 100) return 0;
                return raw/100 - Gold*100;
            }
        }

        public int Copper
        {
            get
            {
                if (raw < 100) return raw;
                return raw - Silver*100 - Gold*10000;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Gold > 0)
            {
                sb.Append(Gold).Append("g ");
            }
            if (Silver > 0)
            {
                sb.Append(Silver).Append("s ");
            }
            sb.Append(Copper).Append("c");
            return sb.ToString();
        }
    }
}