using System;
using System.Collections.Generic;

namespace NKnife.Wrapper
{
    [Flags]
    public enum BytePosition : byte
    {
        BitUnknow = 0xff,
        BitOne = 0x1,
        BitTwo = 0x2,
        BitThree = 0x4,
        BitFour = 0x8,
        BitFive = 0x10,
        BitSix = 0x20,
        BitSeven = 0x40,
        BitEight = 0x80,
    }

    public class BitValue
    {
        public BytePosition Position;
        public bool Value;

        public static BitValue NullValue
        {
            get
            {
                BitValue value = new BitValue
                {
                    Position = BytePosition.BitUnknow,
                    Value = false,
                };
                return value;
            }

        }
    }

    public class ByteClass
    {
        private char[] values = new char[8];
        private Dictionary<string, BitValue> _store = new Dictionary<string, BitValue>();

        public byte ToByte()
        {
            var result = new string(values);
            return Convert.ToByte(result, 2);
        }

        public void SetFromByte(byte bt)
        {
            try
            {
                values = Convert.ToString(bt, 2).ToCharArray();
                foreach (var bitValue in _store.Values)
                {
                    GetValue(bitValue);
                }
            }
            catch
            {

            }
        }

        public bool AddProperty(string key, BitValue value)
        {
            if (_store.Count < 8)
            {
                _store.Add(key, value);
                return true;
            }
            return false;
        }

        public bool TryGetProperty(string key, out BitValue bitValue)
        {
            if (_store.ContainsKey(key))
            {
                bitValue = _store[key];
                return true;
            }
            bitValue = BitValue.NullValue;
            return false;
        }

        public bool SetProperty(string key, bool value)
        {
            if (_store.ContainsKey(key))
            {
                _store[key].Value = value;
                SetValue(_store[key]);
                return true;
            }
            return false;
        }

        private void SetValue(BitValue bv)
        {
            if ((bv.Position & BytePosition.BitOne) == BytePosition.BitOne)
            {
                values[0] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitTwo) == BytePosition.BitTwo)
            {
                values[1] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitThree) == BytePosition.BitThree)
            {
                values[2] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitFour) == BytePosition.BitFour)
            {
                values[3] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitFive) == BytePosition.BitFive)
            {
                values[4] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitSix) == BytePosition.BitSix)
            {
                values[5] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitSeven) == BytePosition.BitSeven)
            {
                values[6] = bv.Value ? '1' : '0';
            }
            if ((bv.Position & BytePosition.BitEight) == BytePosition.BitEight)
            {
                values[7] = bv.Value ? '1' : '0';
            }
        }

        private void GetValue(BitValue bv)
        {
            if ((bv.Position & BytePosition.BitOne) == BytePosition.BitOne)
            {
                bv.Value = values[0] == '1';
            }
            if ((bv.Position & BytePosition.BitTwo) == BytePosition.BitTwo)
            {
                bv.Value = values[1] == '1';
            }
            if ((bv.Position & BytePosition.BitThree) == BytePosition.BitThree)
            {
                bv.Value = values[2] == '1';
            }
            if ((bv.Position & BytePosition.BitFour) == BytePosition.BitFour)
            {
                bv.Value = values[3] == '1';
            }
            if ((bv.Position & BytePosition.BitFive) == BytePosition.BitFive)
            {
                bv.Value = values[4] == '1';
            }
            if ((bv.Position & BytePosition.BitSix) == BytePosition.BitSix)
            {
                bv.Value = values[5] == '1';
            }
            if ((bv.Position & BytePosition.BitSeven) == BytePosition.BitSeven)
            {
                bv.Value = values[6] == '1';
            }
            if ((bv.Position & BytePosition.BitEight) == BytePosition.BitEight)
            {
                bv.Value = values[7] == '1';
            }
        }
    }
}
