using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException();
                }
                NewTotal();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value > 0)
                {
                    nightsCount = value;
                }
                else
                {
                    throw new ArgumentException();
                }
                NewTotal();
                Notify(nameof(NightsCount));
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                if (value >= 0)
                {
                    total = value;
                }
                else
                {
                    throw new ArgumentException();
                }
                NewDiscount();
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                NewTotal();
                Notify(nameof(Discount));
            }
        }

        public void NewDiscount()
        {
            discount = ((-1) * Total / (Price * NightsCount) + 1) * 100;
            Notify(nameof(Discount));
        }

        public void NewTotal()
        {
            if (Price * NightsCount * (1 - Discount / 100) < 0)
            {
                throw new ArgumentException();
            }
            total = Price * NightsCount * (1 - Discount / 100);
            Notify(nameof(Total));
        }
    }
}