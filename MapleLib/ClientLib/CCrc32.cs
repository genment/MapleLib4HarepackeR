﻿/*  MapleLib - A general-purpose MapleStory library
 * Copyright (C) 2023 lastbattle
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

namespace MapleLib.ClientLib {

    /// <summary>
    /// CCrc32::GetCrc32 that MapleStory uses
    /// </summary>
    public unsafe class CCrc32 {

        private static readonly uint[] ms_adwCrc32Table = new uint[]
        {
            0, 0x4C11DB7, 0x9823B6E, 0x0D4326D9, 0x130476DC, 0x17C56B6B,
            0x1A864DB2, 0x1E475005, 0x2608EDB8, 0x22C9F00F, 0x2F8AD6D6,
            0x2B4BCB61, 0x350C9B64, 0x31CD86D3, 0x3C8EA00A, 0x384FBDBD,
            0x4C11DB70, 0x48D0C6C7, 0x4593E01E, 0x4152FDA9, 0x5F15ADAC,
            0x5BD4B01B, 0x569796C2, 0x52568B75, 0x6A1936C8, 0x6ED82B7F,
            0x639B0DA6, 0x675A1011, 0x791D4014, 0x7DDC5DA3, 0x709F7B7A,
            0x745E66CD, 0x9823B6E0, 0x9CE2AB57, 0x91A18D8E, 0x95609039,
            0x8B27C03C, 0x8FE6DD8B, 0x82A5FB52, 0x8664E6E5, 0x0BE2B5B58,
            0x0BAEA46EF, 0x0B7A96036, 0x0B3687D81, 0x0AD2F2D84, 0x0A9EE3033,
            0x0A4AD16EA, 0x0A06C0B5D, 0x0D4326D90, 0x0D0F37027, 0x0DDB056FE,
            0x0D9714B49, 0x0C7361B4C, 0x0C3F706FB, 0x0CEB42022, 0x0CA753D95,
            0x0F23A8028, 0x0F6FB9D9F, 0x0FBB8BB46, 0x0FF79A6F1, 0x0E13EF6F4,
            0x0E5FFEB43, 0x0E8BCCD9A, 0x0EC7DD02D, 0x34867077, 0x30476DC0,
            0x3D044B19, 0x39C556AE, 0x278206AB, 0x23431B1C, 0x2E003DC5,
            0x2AC12072, 0x128E9DCF, 0x164F8078, 0x1B0CA6A1, 0x1FCDBB16,
            0x18AEB13, 0x54BF6A4, 0x808D07D, 0x0CC9CDCA, 0x7897AB07,
            0x7C56B6B0, 0x71159069, 0x75D48DDE, 0x6B93DDDB, 0x6F52C06C,
            0x6211E6B5, 0x66D0FB02, 0x5E9F46BF, 0x5A5E5B08, 0x571D7DD1,
            0x53DC6066, 0x4D9B3063, 0x495A2DD4, 0x44190B0D, 0x40D816BA,
            0x0ACA5C697, 0x0A864DB20, 0x0A527FDF9, 0x0A1E6E04E, 0x0BFA1B04B,
            0x0BB60ADFC, 0x0B6238B25, 0x0B2E29692, 0x8AAD2B2F, 0x8E6C3698,
            0x832F1041, 0x87EE0DF6, 0x99A95DF3, 0x9D684044, 0x902B669D,
            0x94EA7B2A, 0x0E0B41DE7, 0x0E4750050, 0x0E9362689, 0x0EDF73B3E,
            0x0F3B06B3B, 0x0F771768C, 0x0FA325055, 0x0FEF34DE2, 0x0C6BCF05F,
            0x0C27DEDE8, 0x0CF3ECB31, 0x0CBFFD686, 0x0D5B88683, 0x0D1799B34,
            0x0DC3ABDED, 0x0D8FBA05A, 0x690CE0EE, 0x6DCDFD59, 0x608EDB80,
            0x644FC637, 0x7A089632, 0x7EC98B85, 0x738AAD5C, 0x774BB0EB,
            0x4F040D56, 0x4BC510E1, 0x46863638, 0x42472B8F, 0x5C007B8A,
            0x58C1663D, 0x558240E4, 0x51435D53, 0x251D3B9E, 0x21DC2629,
            0x2C9F00F0, 0x285E1D47, 0x36194D42, 0x32D850F5, 0x3F9B762C,
            0x3B5A6B9B, 0x315D626, 0x7D4CB91, 0x0A97ED48, 0x0E56F0FF,
            0x1011A0FA, 0x14D0BD4D, 0x19939B94, 0x1D528623, 0x0F12F560E,
            0x0F5EE4BB9, 0x0F8AD6D60, 0x0FC6C70D7, 0x0E22B20D2, 0x0E6EA3D65,
            0x0EBA91BBC, 0x0EF68060B, 0x0D727BBB6, 0x0D3E6A601, 0x0DEA580D8,
            0x0DA649D6F, 0x0C423CD6A, 0x0C0E2D0DD, 0x0CDA1F604, 0x0C960EBB3,
            0x0BD3E8D7E, 0x0B9FF90C9, 0x0B4BCB610, 0x0B07DABA7, 0x0AE3AFBA2,
            0x0AAFBE615, 0x0A7B8C0CC, 0x0A379DD7B, 0x9B3660C6, 0x9FF77D71,
            0x92B45BA8, 0x9675461F, 0x8832161A, 0x8CF30BAD, 0x81B02D74,
            0x857130C3, 0x5D8A9099, 0x594B8D2E, 0x5408ABF7, 0x50C9B640,
            0x4E8EE645, 0x4A4FFBF2, 0x470CDD2B, 0x43CDC09C, 0x7B827D21,
            0x7F436096, 0x7200464F, 0x76C15BF8, 0x68860BFD, 0x6C47164A,
            0x61043093, 0x65C52D24, 0x119B4BE9, 0x155A565E, 0x18197087,
            0x1CD86D30, 0x29F3D35, 0x65E2082, 0x0B1D065B, 0x0FDC1BEC,
            0x3793A651, 0x3352BBE6, 0x3E119D3F, 0x3AD08088, 0x2497D08D,
            0x2056CD3A, 0x2D15EBE3, 0x29D4F654, 0x0C5A92679, 0x0C1683BCE,
            0x0CC2B1D17, 0x0C8EA00A0, 0x0D6AD50A5, 0x0D26C4D12, 0x0DF2F6BCB,
            0x0DBEE767C, 0x0E3A1CBC1, 0x0E760D676, 0x0EA23F0AF, 0x0EEE2ED18,
            0x0F0A5BD1D, 0x0F464A0AA, 0x0F9278673, 0x0FDE69BC4, 0x89B8FD09,
            0x8D79E0BE, 0x803AC667, 0x84FBDBD0, 0x9ABC8BD5, 0x9E7D9662,
            0x933EB0BB, 0x97FFAD0C, 0x0AFB010B1, 0x0AB710D06, 0x0A6322BDF,
            0x0A2F33668, 0x0BCB4666D, 0x0B8757BDA, 0x0B5365D03, 0x0B1F740B4
        };


        /// <summary>
        /// Calculates the MapleStory CRC value of a signed int32
        /// </summary>
        /// <param name="val">The value to use.</param>
        /// <param name="length">Data size (32 bit = 4, 64 bit = 8)</param>
        /// <param name="initialCrc">The initial value to start with</param>
        /// <param name="xorInitialCrc"></param>
        /// <param name="a5"></param>
        /// <param name="flag2"></param>
        /// <returns></returns>
        public static uint GetCrc32(int input, uint initialValue, bool xorInitialCrc, bool flag2) {
            unsafe {
                // Get a pointer to the first byte of the uint value
                uint result = 0;
                uint crc32 = GetCrc32((byte*)&input, 4, initialValue, xorInitialCrc, ref result, flag2);
                return crc32;
            }
        }

        /// <summary>
        /// Calculates the MapleStory CRC value of a signed int64
        /// </summary>
        /// <param name="val">The value to use.</param>
        /// <param name="length">Data size (32 bit = 4, 64 bit = 8)</param>
        /// <param name="initialCrc">The initial value to start with</param>
        /// <param name="xorInitialCrc"></param>
        /// <param name="a5"></param>
        /// <param name="flag2"></param>
        /// <returns></returns>
        public static uint GetCrc32(long input, uint initialValue, bool xorInitialCrc, bool flag2) {
            unsafe {
                // Get a pointer to the first byte of the uint value
                uint result = 0;
                uint crc32 = GetCrc32((byte*)&input, 8, initialValue, xorInitialCrc, ref result, flag2);
                return crc32;
            }
        }

        /// <summary>
        /// Calculates the MapleStory CRC value
        /// </summary>
        /// <param name="val">The value to use.</param>
        /// <param name="length">Data size (32 bit = 4, 64 bit = 8)</param>
        /// <param name="initialCrc">The initial value to start with</param>
        /// <param name="xorInitialCrc"></param>
        /// <param name="a5"></param>
        /// <param name="flag2"></param>
        /// <returns></returns>
        private static unsafe uint GetCrc32(byte* val, uint length, uint initialCrc, bool xorInitialCrc, ref uint a5, bool flag2) {
            byte* v6 = val;
            if (xorInitialCrc)
                initialCrc ^= (uint)val;
            uint length_ = length;
            uint initialCrc_ = initialCrc;

            if (length < 0 || length >= 16) { // gonna check byte* val pointer somehow.. null terminated byte?
                throw new System.Exception("Invalid length specified.");
            }

            if (length >= 0x10) {
                uint v9 = length >> 4;
                do {
                    uint v10 = ms_adwCrc32Table[v6[1] ^ (((uint)ms_adwCrc32Table[*v6 ^ (initialCrc_ >> 24)] ^ (initialCrc_ << 8)) >> 24)] ^ (((uint)ms_adwCrc32Table[*v6 ^ (initialCrc_ >> 24)] ^ (initialCrc_ << 8)) << 8);
                    byte* v11 = v6 + 1;
                    uint v12 = ms_adwCrc32Table[*++v11 ^ (v10 >> 24)] ^ (v10 << 8);
                    uint v13 = *++v11 ^ (v12 >> 24);
                    int v14 = v11[1];
                    uint v15 = ms_adwCrc32Table[v13] ^ (v12 << 8);
                    ++v11;
                    long v16 = v14 ^ (v15 >> 24);
                    int v17 = v11[1];
                    uint v18 = ms_adwCrc32Table[v16] ^ (v15 << 8);
                    ++v11;
                    long v19 = v17 ^ (v18 >> 24);
                    int v20 = v11[1];
                    uint v21 = ms_adwCrc32Table[v19] ^ (v18 << 8);
                    ++v11;
                    long v22 = v20 ^ (v21 >> 24);
                    int v23 = v11[1];
                    uint v24 = ms_adwCrc32Table[v22] ^ (v21 << 8);
                    ++v11;
                    long v25 = v23 ^ (v24 >> 24);
                    int v26 = v11[1];
                    uint v27 = ms_adwCrc32Table[v25] ^ (v24 << 8);
                    ++v11;
                    long v28 = v26 ^ (v27 >> 24);
                    int v29 = v11[1];
                    uint v30 = ms_adwCrc32Table[v28] ^ (v27 << 8);
                    ++v11;
                    long v31 = v29 ^ (v30 >> 24);
                    int v32 = v11[1];
                    uint v33 = ms_adwCrc32Table[v31] ^ (v30 << 8);
                    ++v11;
                    long v34 = v32 ^ (v33 >> 24);
                    int v35 = v11[1];
                    uint v36 = ms_adwCrc32Table[v34] ^ (v33 << 8);
                    ++v11;
                    long v37 = v35 ^ (v36 >> 24);
                    int v38 = v11[1];
                    uint v39 = ms_adwCrc32Table[v37] ^ (v36 << 8);
                    ++v11;
                    long v40 = v38 ^ (v39 >> 24);
                    int v41 = v11[1];
                    uint v42 = ms_adwCrc32Table[v40] ^ (v39 << 8);
                    ++v11;
                    long v43 = v41 ^ (v42 >> 24);
                    int v44 = v11[1];
                    uint v45 = ms_adwCrc32Table[v43] ^ (v42 << 8);
                    ++v11;
                    uint v46 = ms_adwCrc32Table[v44 ^ (v45 >> 24)] ^ (v45 << 8);
                    uint v47 = ms_adwCrc32Table[v11[1] ^ (v46 >> 24)] ^ (v46 << 8);
                    v6 = v11 + 2;
                    length_ -= 0x10;
                    --v9;
                    initialCrc_ = v47;
                } while (v9 != 0);
            }

            while (length_ > 0) {
                initialCrc_ = ms_adwCrc32Table[*v6++ ^ (initialCrc_ >> 24)] ^ (initialCrc_ << 8);
                --length_;
            }

            if (xorInitialCrc && flag2) {
                //  int v48 = CCrc32.dword_E0DB10;
                //   *(uint*)(v48 + 132) = _ZtlSecureTear<long>(v6, CCrc32.dword_E0DB10 + 124);
            }

            //if (a5 != null)
                a5 = 7;

            return initialCrc_;
        }
    }
}
