﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DS2S_META.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DS2S_META.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  83 ec 28                sub    esp,0x28
        ///3:  b9 70 70 70 70          mov    ecx,0x70707070	// PlayerParam
        ///8:  b8 70 70 70 70          mov    eax,0x70707070	// Number of Souls
        ///d:  50                      push   eax
        ///e:  b8 70 70 70 70          mov    eax,0x70707070	// funcGiveSouls
        ///13: ff d0                   call   eax
        ///15: 83 c4 28                add    esp,0x28
        ///18: c3                      ret.
        /// </summary>
        internal static string AddSouls32 {
            get {
                return ResourceManager.GetString("AddSouls32", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  48 83 ec 28             sub    rsp,0x28 
        ///4:  48 b9 00 00 00 00 ff    movabs rcx,0xffffffff00000000 ;PlayerParam Pointer
        ///b:  ff ff ff
        ///e:  48 c7 c2 f4 01 00 00    mov    rdx,0x1f4 ;number of souls
        ///15: 49 be 00 00 00 00 ff    movabs r14,0xffffffff00000000 ;Give Souls func
        ///1c: ff ff ff
        ///1f: 41 ff d6                call   r14
        ///22: 48 83 c4 28             add    rsp,0x28
        ///26: c3                      ret .
        /// </summary>
        internal static string AddSouls64 {
            get {
                return ResourceManager.GetString("AddSouls64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  89 e5                   mov    ebp,esp
        ///2:  83 ec 1C                sub    esp,0x18
        ///5:  c7 44 24 04 70 70 70    mov    DWORD PTR [esp+0x4],0x70707070	// SpEfID
        ///c:  70
        ///d:  c7 44 24 08 01 00 00    mov    DWORD PTR [esp+0x8],0x1
        ///14: 00
        ///15: b8 70 70 70 70          mov    eax,0x70707070					// &amp;-1f
        ///1a: f3 0f 10 00             movss  xmm0,DWORD PTR [eax]
        ///1e: f3 0f 11 44 24 0c       movss  DWORD PTR [esp+0xc],xmm0
        ///24: 66 c7 44 24 10 19 02    mov    WORD PTR [esp+0x10],0x219
        ///2b: 8d 44 24 04             [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ApplySpecialEffect32 {
            get {
                return ResourceManager.GetString("ApplySpecialEffect32", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  48 83 ec 38             sub    rsp,0x38
        ///4:  48 ba 00 00 00 00 ff    movabs rdx,0xffffffff00000000 (0x6_pEffectStruct)
        ///b:  ff ff ff
        ///e:  48 b9 00 00 00 00 ff    movabs rcx,0xffffffff00000000 (0x10_SpEfCtrl)
        ///15: ff ff ff
        ///18: 48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000 (0x1A_pfloat_-1.0)
        ///1f: ff ff ff
        ///22: f3 0f 10 00             movss  xmm0,DWORD PTR [rax]
        ///26: f3 0f 11 44 24 28       movss  DWORD PTR [rsp+0x28],xmm0
        ///2c: 48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000 (0x2E_pfuncApply [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ApplySpecialEffect64 {
            get {
                return ResourceManager.GetString("ApplySpecialEffect64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  89 e5                   mov    ebp,esp
        ///2:  81 ec b8 00 00 00       sub    esp,0xb8
        ///8:  6a 02                   push   0x2						// assume default_area_warp until told otherwise
        ///a:  68 70 70 70 70          push   0x70707070				// BonfireID
        ///f:  8d 55 b0                lea    edx,[ebp-0x50]
        ///12: 52                      push   edx
        ///13: b8 70 70 70 70          mov    eax,0x70707070			// funcSetWarpTarget
        ///18: ff d0                   call   eax
        ///1a: 8d 45 b0                lea    eax,[ebp-0x50]
        ///1d: c7 00  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BonfireWarp32 {
            get {
                return ResourceManager.GetString("BonfireWarp32", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  48 81 ec 10 01 00 00    sub    rsp,0x110
        ///7:  48 ba 00 00 00 00 ff    movabs rdx,0xffffffff00000000
        ///e:  ff ff ff
        ///11: 0f b7 12                movzx  edx,WORD PTR [rdx]
        ///14: 48 8d 4c 24 50          lea    rcx,[rsp+0x50]
        ///19: 41 b8 02 00 00 00       mov    r8d,0x2					// default to area start if this value is NOT updated to 3 inside warpman
        ///1f: 49 be 00 00 00 00 ff    movabs r14,0xffffffff00000000
        ///26: ff ff ff
        ///29: 41 ff d6                call   r14						// call 001811d0_warpFunctionManager(rcx=*retWar [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BonfireWarp64 {
            get {
                return ResourceManager.GetString("BonfireWarp64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //Format: ID; SL VRG END VIT ATN STR DEX ADP INT FTH; Name [arbitrary]
        ///01 12 07 06 06 05 15 11 05 05 05 Warrior
        ///02 13 12 06 07 04 11 08 09 03 06 Knight
        ///04 11 09 07 11 02 09 14 03 01 08 Bandit
        ///06 14 10 03 08 10 11 05 04 04 12 Cleric
        ///07 11 05 06 05 12 03 07 08 14 04 Sorcerer
        ///08 10 07 06 09 07 06 06 12 05 05 Explorer
        ///09 12 04 08 04 06 09 16 06 07 05 Swordsman
        ///10 01 06 06 06 06 06 06 06 06 06 Deprived.
        /// </summary>
        internal static string Classes {
            get {
                return ResourceManager.GetString("Classes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  81 ec e8 01 00 00       sub    esp,0x1e8
        ///6:  b8 01 00 00 00          mov    eax,0x1
        ///b:  ba 70 70 70 70          mov    edx,0x70707070
        ///10: b9 70 70 70 70          mov    ecx,0x70707070
        ///15: 6a 00                   push   0x0
        ///17: 50                      push   eax
        ///18: 52                      push   edx
        ///19: b8 70 70 70 70          mov    eax,0x70707070
        ///1e: ff d0                   call   eax
        ///20: b9 01 00 00 00          mov    ecx,0x1			// NumItems (unique pickup things)
        ///25: 6a 01                    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GiveItemWithMenu32 {
            get {
                return ResourceManager.GetString("GiveItemWithMenu32", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  48 81 ec e8 01 00 00    sub    rsp,0x1e8
        ///7:  41 b8 08 00 00 00       mov    r8d,0x8 ;Number of items
        ///d:  49 bf 00 00 00 00 ff    movabs r15,0xffffffff00000000 ;Item Struct Address
        ///14: ff ff ff
        ///17: 49 8d 17                lea    rdx,[r15]
        ///1a: 48 b9 00 00 00 00 ff    movabs rcx,0xffffffff00000000 ;Item bag?
        ///21: ff ff ff
        ///24: 45 31 c9                xor    r9d,r9d
        ///27: 49 be 00 00 00 00 ff    movabs r14,0xffffffff00000000 ;Call add item function DarkSoulsII.exe+1A8C67
        ///2e: ff ff ff
        ///31: 41 ff d6      [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GiveItemWithMenu64 {
            get {
                return ResourceManager.GetString("GiveItemWithMenu64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  48 83 ec 28             sub    rsp,0x28
        ///4:  41 b8 08 00 00 00       mov    r8d,0x8
        ///a:  49 bf 00 00 00 00 ff    movabs r15,0xffffffff00000000 ;Item Struct Address
        ///11: ff ff ff
        ///14: 49 8d 17                lea    rdx,[r15]
        ///17: 48 b9 00 00 00 00 ff    movabs rcx,0xffffffff00000000 ;Item bag?
        ///1e: ff ff ff
        ///21: 45 31 c9                xor    r9d,r9d
        ///24: 49 be 00 00 00 00 ff    movabs r14,0xffffffff00000000 ;Call add item function DarkSoulsII.exe+1A8C67
        ///2b: ff ff ff
        ///2e: 41 ff d6                call    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GiveItemWithoutMenu {
            get {
                return ResourceManager.GetString("GiveItemWithoutMenu", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  f3 0f 59 99 a8 02 00    mulss  xmm3,DWORD PTR [rcx+0x2a8]
        ///7:  00
        ///8:  f3 0f 10 12             movss  xmm2,DWORD PTR [rdx]
        ///c:  f3 0f 10 42 04          movss  xmm0,DWORD PTR [rdx+0x4] .
        /// </summary>
        internal static string OgSpeedFactor {
            get {
                return ResourceManager.GetString("OgSpeedFactor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  f3 0f 59 9f a8 02 00    mulss  xmm3,DWORD PTR [rdi+0x2a8]
        ///7:  00
        ///8:  f3 0f 10 16             movss  xmm2,DWORD PTR [rsi]
        ///c:  f3 0f 10 46 04          movss  xmm0,DWORD PTR [rsi+0x4] .
        /// </summary>
        internal static string OgSpeedFactorAccel {
            get {
                return ResourceManager.GetString("OgSpeedFactorAccel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  50                      push   rax
        ///1:  48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000
        ///8:  ff ff ff
        ///b:  ff d0                   call   rax
        ///d:  58                      pop    rax
        ///e:  90                      nop
        ///f:  90                      nop
        ///10: 90                      nop
        ///11: 48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000
        ///18: ff ff ff
        ///1b: f3 0f 59 18             mulss  xmm3,DWORD PTR [rax]
        ///1f: f3 0f 10 12             movss  xmm2,DWORD PTR [rdx]
        ///23: f3 0f 10 42 04          movss [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SpeedFactor {
            get {
                return ResourceManager.GetString("SpeedFactor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0:  50                      push   rax
        ///1:  48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000
        ///8:  ff ff ff
        ///b:  ff d0                   call   rax
        ///d:  58                      pop    rax
        ///e:  90                      nop
        ///f:  90                      nop
        ///10: 90                      nop
        ///11: 48 b8 00 00 00 00 ff    movabs rax,0xffffffff00000000
        ///18: ff ff ff
        ///1b: f3 0f 59 18             mulss  xmm3,DWORD PTR [rax]
        ///1f: f3 0f 10 16             movss  xmm2,DWORD PTR [rsi]
        ///23: f3 0f 10 46 04          movss [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SpeedFactorAccel {
            get {
                return ResourceManager.GetString("SpeedFactorAccel", resourceCulture);
            }
        }
    }
}
