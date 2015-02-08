using System.Text;

namespace Gw2spidyApi.Objects
{
    public class Currency : Gw2Object
    {
        public int Raw { get; private set; }

        public Currency(int raw)
        {
            Raw = raw;
        }

        public int Gold
        {
            get
            {
                if (Raw < 10000) return 0;
                return Raw / 10000;
            }
        }

        public int Silver
        {
            get
            {
                if (Raw < 100) return 0;
                return Raw / 100 - Gold * 100;
            }
        }

        public int Copper
        {
            get
            {
                if (Raw < 100) return Raw;
                return Raw - Silver * 100 - Gold * 10000;
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

        public static implicit operator int(Currency obj)
        {
            return obj.Raw;
        }

        public static implicit operator Currency(int raw)
        {
            return new Currency(raw);
        }

        public static Currency operator +(Currency a, Currency b)
        {
            return new Currency(a.Raw + b.Raw);
        }

        public static Currency operator -(Currency a, Currency b)
        {
            return new Currency(a.Raw - b.Raw);
        }

        public static Currency operator *(Currency a, float b)
        {
            return new Currency((int) (a.Raw * b));
        }

        public static Currency operator /(Currency a, float b)
        {
            return new Currency((int) (a.Raw / b));
        }
    }
}