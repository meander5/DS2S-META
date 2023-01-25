﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2S_META.Utils.Offsets
{
    internal class DS2VOffsetsV102 : DS2VOffsets
    {
        public DS2VOffsetsV102() : base()
        {
            if (Func == null)
                return;
            
            Func.ApplySpEffectAoB = "E9 ? ? ? ? 8B 45 F4 83 C0 01 89 45 F4 E9 ? ? ? ?";
            Func.ItemStruct2dDisplay = "55 8b ec 8b 45 08 8b 4d 14 53 8b 5d 10 56 33 f6";
            Func.GiveSoulsFuncAoB = "55 8b ec 8b 81 e8 00 00 00 8b 55 08 83 ec 08 56";

        }
    }
}
