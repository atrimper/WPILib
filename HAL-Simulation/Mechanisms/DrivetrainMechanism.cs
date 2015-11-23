﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Mechanisms
{
    public class TankDriveTrainMechanism
    {
        private DriveWheelMechanism m_leftDrive;
        private DriveWheelMechanism m_rightDrive;
        private double m_mass;

        private double[] WorldPos = {0.0, 0.0, 0.0};
        private double[] BotVel = {0.0, 0.0, 0.0};

        public TankDriveTrainMechanism(DriveWheelMechanism leftDrive, DriveWheelMechanism rightDrive, double massKg)
        {
            m_leftDrive = leftDrive;
            m_rightDrive = rightDrive;
            m_mass = massKg;
        }

        public void Update(double seconds)
        {
            double[] driveAcceleration = {0.0, 0.0, 0.0};

            m_leftDrive.Update(seconds, driveAcceleration, m_mass, 2, BotVel);
            m_rightDrive.Update(seconds, driveAcceleration, m_mass, 2, BotVel);


            BotVel[0] = BotVel[0] + driveAcceleration[0] * seconds;
            BotVel[1] = BotVel[1] + driveAcceleration[1] * seconds;
            BotVel[2] = BotVel[2] + driveAcceleration[2] * seconds;

            double angle = WorldPos[2] + BotVel[2] * seconds;
            double xDelta = Math.Sin(angle) + BotVel[0] * seconds;
            double yDelta = Math.Sin(angle) + BotVel[0] * seconds;

            WorldPos[0] += xDelta;
            WorldPos[1] += yDelta;
            WorldPos[2] += angle;
        }
    }
}
