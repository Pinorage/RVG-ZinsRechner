using System;

namespace RVG_ZinsRechner
{
    public static class AttorneyFeeTable
    {
        public static float[,] attorneyFeeTable = new float[,] { { 500, 45.00f }, { 1000, 80.00f }, { 1500, 115.00f },
            { 2000, 150.00f }, { 3000, 201.00f }, { 4000, 252.00f }, { 5000, 303.00f }, { 6000, 354.00f }, { 7000, 405.00f },
            { 8000, 456.00f }, { 9000, 507.00f }, { 10000, 558.00f }, { 13000, 604.00f }, { 16000, 650.00f }, { 19000, 696.00f },
            { 22000, 742.00f }, { 25000, 788.00f }, { 30000, 863.00f }, { 35000, 938.00f }, { 40000, 1013.00f }, { 45000, 1088.00f },
            { 50000, 1163.00f }, { 65000, 1248.00f }, { 80000, 1333.00f }, { 95000, 1418.00f }, { 110000, 1503.00f }, { 125000, 1588.00f },
            { 140000, 1673.00f }, { 155000, 1758.00f }, { 170000, 1843.00f }, { 185000, 1928.00f }, { 200000, 2013.00f }, { 230000, 2133.00f },
            { 260000, 2253.00f }, { 290000, 2373.00f }, { 320000, 2493.00f }, { 350000, 2613.00f }, { 380000, 2733.00f }, { 410000, 2853.00f },
            { 440000, 2973.00f }, { 470000, 3093.00f }, { 500000, 3213.00f }
        };


        public static string[,] _Basiszinssatz = new string[,] { {"01.07.2012", "31.12.2012","0,12"},
                                                                 {"01.01.2013", "30.06.2013", "-0,13"},
                                                                 {"01.07.2013", "31.12.2013", "-0,38"},
                                                                 {"01.01.2014", "30.06.2014", "-0,63"},
                                                                 {"01.07.2014", "28.07.2014", "-0,73"},
                                                                 {"29.07.2014", "31.12.2014", "-0,73"},
                                                                 {"01.01.2015", "30.06.2015", "-0,83"},
                                                                 {"01.07.2015", "31.12.2015", "-0,83"},
                                                                 {"01.01.2016", "30.06.2016", "-0,83"},
                                                                 {"01.07.2016", "31.12.2016", "-0,88"},
                                                                 {"01.01.2017", "30.06.2017", "-0,88"},
                                                                 {"01.07.2017", "31.12.2017", "-0,88"},
                                                                 {"01.01.2018", "30.06.2018", "-0,88"},
                                                                 {"01.07.2018", "31.12.2018", "-0,88"},
                                                                 {"01.01.2019", "30.06.2019", "-0,88"},
                                                                 {"01.07.2019", "31.12.2019", "-0,88"},
                                                                 {"01.01.2020", "30.06.2020", "-0,88"},
                                                                 {"01.07.2020", "31.12.2020", "-0,88"},
                                                                 {"01.01.2021", "30.06.2021", "-0,88"},
                                                                 {"01.07.2021", "31.12.2021", "-0,88"}};



        public static double _calculateDefaultInterest(decimal valueInDispute, decimal zins, DateTime startDate, DateTime endDate)
        {
            int days = (endDate - startDate).Days + 1;
           // decimal currentBasiszinssatz = 0.12m;
            double _defaultInterest = 0.0;
            DateTime _currentDate;

            for (int i = 0; i < days; i++) {

                _currentDate = startDate.AddDays(i);

                for (int j = 0; j < _Basiszinssatz.GetLength(0); j++)
                {
                    if ((_currentDate >= DateTime.Parse(_Basiszinssatz[j, 0])) &&
                       (_currentDate <= DateTime.Parse(_Basiszinssatz[j, 1])))
                    {
                        double currentZins = Double.Parse(_Basiszinssatz[j, 2]); 
                        _defaultInterest += Math.Round((double)valueInDispute * ((double)zins + currentZins) / 100 / 365 * 1,4);
                 
                    }
                }
            
            }

            return _defaultInterest;
        }

        public static decimal _old_calculateAttorneyFee(float valueInDispute, float attorneyFee)
        {
            int setter = 0;
            for (int i = 0; i < 42; i++)
            {
                if (valueInDispute < attorneyFeeTable[0, 0])
                {
                    setter = 0;
                    break;
                }
                else if (i == 41)
                {
                    setter = i;
                    break;
                }
                else if (valueInDispute > attorneyFeeTable[i, 0] &&
                  valueInDispute < attorneyFeeTable[i + 1, 0])
                {
                    setter = i + 1;
                    break;
                }
            }
            return System.Math.Round((decimal)(attorneyFeeTable[setter, 1] * attorneyFee), 2,
                System.MidpointRounding.AwayFromZero);
        }
        public static decimal _old_calculateAttorneyFeeV2(float valueInDispute, float attorneyFee)
        {
            int setter = 0;
            decimal returnFee = 0;
            while (setter != 41 && valueInDispute > attorneyFeeTable[setter, 0])
            {
                setter++;
            }
            returnFee = (decimal)(attorneyFeeTable[setter, 1] * attorneyFee);
            if (setter == 41 && valueInDispute > 500000)
            {
                returnFee += (decimal)(((int)((int)valueInDispute - 500000) / 50000) * 150);
            }
            return System.Math.Round(returnFee, 2, System.MidpointRounding.AwayFromZero);
        }

        public static decimal calculateAttorneyFee_2021(decimal valueInDispute, decimal attorneyFee)
        {
            decimal baseFee = 49;
            decimal borderFee = 500;

            while (borderFee < valueInDispute)
            {

                if (borderFee < 2000)
                {
                    borderFee += 500;
                    baseFee += 39;
                }
                else if (borderFee < 10000)
                {
                    borderFee += 1000;
                    baseFee += 56;
                }
                else if (borderFee < 25000)
                {
                    borderFee += 3000;
                    baseFee += 52;
                }
                else if (borderFee < 50000)
                {
                    borderFee += 5000;
                    baseFee += 81;
                }
                else if (borderFee < 200000)
                {
                    borderFee += 15000;
                    baseFee += 94;
                }
                else if (borderFee < 500000)
                {
                    borderFee += 30000;
                    baseFee += 132;
                }
                else
                {
                    borderFee += 50000;
                    baseFee += 165;
                }
            }
            return baseFee * attorneyFee;
        }

        public static decimal calculateAttorneyFee_2020(decimal valueInDispute, decimal attorneyFee)
        {
            decimal baseFee = 45;
            decimal borderFee = 500;

            while (borderFee < valueInDispute)
            {

                if (borderFee < 2000)
                {
                    borderFee += 500;
                    baseFee += 35;
                }
                else if (borderFee < 10000)
                {
                    borderFee += 1000;
                    baseFee += 51;
                }
                else if (borderFee < 25000)
                {
                    borderFee += 3000;
                    baseFee += 46;
                }
                else if (borderFee < 50000)
                {
                    borderFee += 5000;
                    baseFee += 75;
                }
                else if (borderFee < 200000)
                {
                    borderFee += 15000;
                    baseFee += 85;
                }
                else if (borderFee < 500000)
                {
                    borderFee += 30000;
                    baseFee += 120;
                }
                else
                {
                    borderFee += 50000;
                    baseFee += 150;
                }
            }
            return baseFee * attorneyFee;
        }

        public static decimal calculateCourtFee_2020(decimal valueInDispute, decimal attorneyFee)
        {
            decimal baseFee = 35;
            decimal borderFee = 500;

            while (borderFee < valueInDispute)
            {

                if (borderFee < 2000)
                {
                    borderFee += 500;
                    baseFee += 18;
                }
                else if (borderFee < 10000)
                {
                    borderFee += 1000;
                    baseFee += 19;
                }
                else if (borderFee < 25000)
                {
                    borderFee += 3000;
                    baseFee += 26;
                }
                else if (borderFee < 50000)
                {
                    borderFee += 5000;
                    baseFee += 35;
                }
                else if (borderFee < 200000)
                {
                    borderFee += 15000;
                    baseFee += 120;
                }
                else if (borderFee < 500000)
                {
                    borderFee += 30000;
                    baseFee += 179;
                }
                else
                {
                    borderFee += 50000;
                    baseFee += 180;
                }
            }
            return baseFee * attorneyFee;
        }

        public static decimal calculateCourtFee_2021(decimal valueInDispute, decimal attorneyFee)
        {
            decimal baseFee = 38;
            decimal borderFee = 500;

            while (borderFee < valueInDispute)
            {

                if (borderFee < 2000)
                {
                    borderFee += 500;
                    baseFee += 20;
                }
                else if (borderFee < 10000)
                {
                    borderFee += 1000;
                    baseFee += 21;
                }
                else if (borderFee < 25000)
                {
                    borderFee += 3000;
                    baseFee += 29;
                }
                else if (borderFee < 50000)
                {
                    borderFee += 5000;
                    baseFee += 38;
                }
                else if (borderFee < 200000)
                {
                    borderFee += 15000;
                    baseFee += 132;
                }
                else if (borderFee < 500000)
                {
                    borderFee += 30000;
                    baseFee += 198;
                }
                else
                {
                    borderFee += 50000;
                    baseFee += 198;
                }
            }
            return baseFee * attorneyFee;
        }

        public static bool ValidDateFeeTable()
        {
            bool validator = true;
            for (int i = 1; i < 42; i++)
            {

                if (attorneyFeeTable[i, 0] > attorneyFeeTable[i - 1, 0])
                {
                    validator = true;
                }
                else
                {
                    validator = false;
                    System.Console.WriteLine("False --> Index (" + i.ToString() + ") --> " +
                        attorneyFeeTable[i, 0].ToString());
                }
            }
            return validator;
        }
    }
}