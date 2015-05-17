﻿using HAL_Base;


namespace HAL_RoboRIO
{
    public class HALInterrupts
    {
        /// Return Type: void*
        ///interruptIndex: unsigned int
        ///watcher: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "initializeInterrupts")]
        public static extern System.IntPtr initializeInterrupts(uint interruptIndex, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool watcher, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "cleanInterrupts")]
        public static extern void cleanInterrupts(System.IntPtr interruptPointer, ref int status);


        /// Return Type: unsigned int
        ///interrupt_pointer: void*
        ///timeout: double
        ///ignorePrevious: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "waitForInterrupt")]
        public static extern uint waitForInterrupt(System.IntPtr interruptPointer, double timeout, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool ignorePrevious, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "enableInterrupts")]
        public static extern void enableInterrupts(System.IntPtr interruptPointer, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "disableInterrupts")]
        public static extern void disableInterrupts(System.IntPtr interruptPointer, ref int status);


        /// Return Type: double
        ///interrupt_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "readRisingTimestamp")]
        public static extern double readRisingTimestamp(System.IntPtr interruptPointer, ref int status);


        /// Return Type: double
        ///interrupt_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "readFallingTimestamp")]
        public static extern double readFallingTimestamp(System.IntPtr interruptPointer, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///routing_module: byte
        ///routing_pin: unsigned int
        ///routing_analog_trigger: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "requestInterrupts")]
        public static extern void requestInterrupts(System.IntPtr interruptPointer, byte routingModule, uint routingPin, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool routingAnalogTrigger, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///handler: InterruptHandlerFunction
        ///param: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "attachInterruptHandler")]
        public static extern void attachInterruptHandler(System.IntPtr interruptPointer, InterruptHandlerFunctionHAL handler, System.IntPtr param, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "setInterruptUpSourceEdge")]
        public static extern void setInterruptUpSourceEdge(System.IntPtr interruptPointer, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
