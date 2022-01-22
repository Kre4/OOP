using System;
using Banks.Tools;

namespace Banks.DTO
{
    public class PassportData
    {
        private string _serial;
        private string _number;

        public PassportData(string serial, string number)
        {
            CheckSerial(serial);
            CheckNumber(number);

            _serial = serial;
            _number = number;
        }

        public PassportData DeepCopy()
        {
            return new PassportData(_serial, _number);
        }

        public string GetSerial() => _serial;
        public string GetNumber() => _number;
        private void CheckSerial(string serial)
        {
            try
            {
                int serialInt = Convert.ToInt32(serial);
                if (serialInt < 0 || serial.Length != Consts.SerialLength)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new BankException("Incorrect serial of client's passport");
            }
        }

        private void CheckNumber(string number)
        {
            try
            {
                int numberInt = Convert.ToInt32(number);
                if (numberInt < 0 || number.Length != Consts.NumberLength)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new BankException("Incorrect number of client's passport");
            }
        }
    }
}