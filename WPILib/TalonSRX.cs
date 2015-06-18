﻿using System;
using HAL_Base;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Cross the Road Electronics (CTRE) Talon SRX Speed Controller with PWM control</summary>
    /// <seealso cref="CANTalon"> for CAN control of Talon SRX</seealso>
    public class TalonSRX : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary><remarks>
        /// Note that the TalonSRX uses the following bounds for PWM values. These values should work reasonably well for
        /// most controllers, but if users experience issues such as asymmetric behavior around
        /// the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// The calibration procedure can be found in the TalonSRX User Manual available from CTRE.
        /// <para> </para>
        /// <para />  - 2.004ms = full "forward"
        /// <para />  - 1.52ms = the "high end" of the deadband range
        /// <para />  - 1.50ms = center of the deadband range (off)
        /// <para />  - 1.48ms = the "low end" of the deadband range
        /// <para />  - .997ms = full "reverse"
        /// </remarks>
        protected void InitTalonSRX()
        {
            SetBounds(2.004, 1.52, 1.50, 1.48, .997);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            LiveWindow.AddActuator(nameof(TalonSRX), Channel, this);
            HAL.Report(ResourceType.kResourceType_TalonSRX, (byte) Channel);
        }

        /// <summary>
        /// Creates a new Talon SRX Motor Controller in PWM mode.
        /// </summary>
        /// <remarks>See <see cref="CANTalon"/> for using a TalonSRX in CAN mode.</remarks>
        /// <param name="channel">The PWM Channel that the TalonSRX is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public TalonSRX(int channel)
            : base(channel)
        {
            InitTalonSRX();
        }
    }

    /*
        /// <summary>
        /// Cross the Road Electronics (CTRE) Talon SRX Speed Controller with PWM control</summary>
        /// <seealso cref="CANTalon"> for CAN control of Talon SRX</seealso>
        public class TalonSRX : SafePWM, ISpeedController
        {
            /// <summary>
            /// Common initialization code called by all constructors.
            /// </summary><remarks>
            /// Note that the TalonSRX uses the following bounds for PWM values. These values should work reasonably well for
            /// <para />most controllers, but if users experience issues such as asymmetric behavior around
            /// <para />the deadband or inability to saturate the controller in either direction, calibration is recommended.
            /// <para />The calibration procedure can be found in the TalonSRX User Manual available from CTRE.
            /// <para> </para>
            /// <para />  - 2.004ms = full "forward"
            /// <para />  - 1.52ms = the "high end" of the deadband range
            /// <para />  - 1.50ms = center of the deadband range (off)
            /// <para />  - 1.48ms = the "low end" of the deadband range
            /// <para />  - .997ms = full "reverse"
            /// </remarks>
            protected void InitTalonSRX()
            {
                SetBounds(2.004, 1.52, 1.50, 1.48, .997);
                PeriodMultiplier = PeriodMultiplier.K1X;
                SetRaw(CenterPwm);
                SetZeroLatch();

                LiveWindow.AddActuator("TalonSRX", Channel, this);
                HAL.Report(ResourceType.kResourceType_TalonSRX, (byte)Channel);
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="channel">The PWM Channel that the TalonSRX is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
            public TalonSRX(int channel)
                : base(channel)
            {
                InitTalonSRX();
            }

            /// <summary>
            /// Write out the PID value as seen in the PIDOutput base object.
            /// </summary>
            /// <param name="value">Write out the PWM value at it was found in the PID Controller</param>
            public void PidWrite(double value)
            {
                Set(value);
            }

            /// <summary>
            /// Get the recently set value of the PWM.
            /// </summary>
            /// <param name="value">The most recently set value for the PWM between -1.0 and 1.0</param>
            public void Set(double value)
            {
                SetSpeed(value);
                Feed();
            }

            /// <summary>
            /// Get the recently set value of the PWM.
            /// </summary>
            /// <returns>The most recently set value for the PWM between -1.0 and 1.0</returns>
            public double Get()
            {
                return GetSpeed();
            }

            /// <summary>
            /// Set the PWM value.
            /// </summary> <remarks>
            /// <para> </para>
            /// The PWM value is set using a range of -1.0 to 1.0, appropriately
            /// scaling the value for the FPGA.
            /// </remarks>
            /// <param name="value">The speed to set. Value should be between -1.0 and 1.0</param>
            /// <param name="syncGroup">The update group to add this Set() to, pending UpdateSyncGroup(). If 0, update immediately.</param>
            [Obsolete("For compatibility with CAN Jaguar")]
            public void Set(double value, byte syncGroup)
            {
                SetSpeed(value);
                Feed();
            }
        }
        */
    }
