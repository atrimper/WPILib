﻿using System;
using HAL_Base;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// VEX Robotics Victor 888 Speed Controller
    /// </summary><remarks>
    /// The Vex Robotics Victor 884 Speed Controller can also be used with this
    /// class but may need to be calibrated per the Victor 884 user manual.
    /// </remarks>
    public class Victor : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary><remarks>
        /// Note that the Victor uses the following bounds for PWM values.  These values were determined
        /// empirically and optimized for the Victor 888. These values should work reasonably well for
        /// Victor 884 controllers also but if users experience issues such as asymmetric behaviour around
        /// the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// The calibration procedure can be found in the Victor 884 User Manual available from VEX Robotics:
        /// http://content.vexrobotics.com/docs/ifi-v884-users-manual-9-25-06.pdf
        /// <para> </para>
        /// <para />  - 2.027ms = full "forward"
        /// <para />  - 1.525ms = the "high end" of the deadband range
        /// <para />  - 1.507ms = center of the deadband range (off)
        /// <para />  - 1.49ms = the "low end" of the deadband range
        /// <para />  - 1.026ms = full "reverse"
        /// </remarks>
        protected void InitVictor()
        {
            SetBounds(2.027, 1.525, 1.507, 1.49, 1.026);
            PeriodMultiplier = PeriodMultiplier.K2X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            LiveWindow.AddActuator(nameof(Victor), Channel, this);
            HAL.Report(ResourceType.kResourceType_Victor, (byte) Channel);
        }

        /// <summary>
        /// Creates a new Victor 888 Motor Controller.
        /// </summary>
        /// <remarks>Please see <see cref="VictorSP"/> for PWM control of VictorSP motor controllers.</remarks>
        /// <param name="channel">The PWM Channel that the Victor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Victor(int channel)
            : base(channel)
        {
            InitVictor();
        }
    }

    /*
        /// <summary>
        /// VEX Robotics Victor 888 Speed Controller
        /// </summary><remarks>
        /// The Vex Robotics Victor 884 Speed Controller can also be used with this
        /// class but may need to be calibrated per the Victor 884 user manual.
        /// </remarks>
        public class Victor : SafePWM, ISpeedController
        {
            /// <summary>
            /// Common initialization code called by all constructors.
            /// </summary><remarks>
            /// Note that the Victor uses the following bounds for PWM values.  These values were determined
            /// <para /> empirically and optimized for the Victor 888. These values should work reasonably well for
            /// <para /> Victor 884 controllers also but if users experience issues such as asymmetric behaviour around
            /// <para /> the deadband or inability to saturate the controller in either direction, calibration is recommended.
            /// <para /> The calibration procedure can be found in the Victor 884 User Manual available from VEX Robotics:
            /// <para /> http://content.vexrobotics.com/docs/ifi-v884-users-manual-9-25-06.pdf
            /// <para> </para>
            /// <para />  - 2.027ms = full "forward"
            /// <para />  - 1.525ms = the "high end" of the deadband range
            /// <para />  - 1.507ms = center of the deadband range (off)
            /// <para />  - 1.49ms = the "low end" of the deadband range
            /// <para />  - 1.026ms = full "reverse"
            /// </remarks>
            protected void InitVictor()
            {
                SetBounds(2.027, 1.525, 1.507, 1.49, 1.026);
                PeriodMultiplier = PeriodMultiplier.K2X;
                SetRaw(CenterPwm);
                SetZeroLatch();

                LiveWindow.AddActuator("Victor", Channel, this);
                HAL.Report(ResourceType.kResourceType_Victor, (byte)Channel);
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="channel">The PWM Channel that the Victor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
            public Victor(int channel)
                : base(channel)
            {
                InitVictor();
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
            /// Set the PWM value for a specific sync group
            /// </summary> <remarks>
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
