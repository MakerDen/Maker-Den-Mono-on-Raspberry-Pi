﻿using Raspberry.IO.InterIntegratedCircuit;
using System;
using System.Threading;

namespace Glovebox.Adafruit.Mini8x8Matrix {
    public class AdaFruitMatrixRun : Adafruit8x8Matrix {

        Thread matrixThread;

        public AdaFruitMatrixRun(I2cDeviceConnection connection, string name)
            : base(new Ht16K33I2cConnection(connection), name) {

            CreateCyclesCollection();

            matrixThread = new Thread(new ThreadStart(this.RunSequence));
            matrixThread.Start();
        }


        private void CreateCyclesCollection() {
            cycles = new DoCycle[] {

          //  new DoCycle(HappyBirthday),
            new DoCycle(AlphaNumeric),
            new DoCycle(ShowSymbols),
            new DoCycle(Hearts),
            new DoCycle(Random),
            };
        }

        private void HappyBirthday() {
            ScrollStringInFromRight("Happy Birthday", 50);
            ScrollSymbolInFromRight(new Symbols[] { Symbols.Heart, Symbols.Heart }, 50);
        }

        private void AlphaNumeric() {
            for (int i = 0; i < fontSimple.Length; i++) {
                DrawBitmap(fontSimple[i]);
                FrameDraw();
                Thread.Sleep(50);
            }
        }

        private void ShowSymbols() {
            foreach (Symbols suit in Enum.GetValues(typeof(Symbols))) {
                DrawSymbol(suit);
                FrameDraw();
                Thread.Sleep(1000);
            }
        }

        private void Hearts() {
            DrawSymbol(Symbols.Heart);
            FrameDraw();
            Thread.Sleep(50);

            for (int i = 0; i < 4; i++) {
                for (ushort c = 0; c < Columns; c++) {
                    FrameRollRight();
                    FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int c = 0; c < 4; c++) {
                for (int i = 0; i < Rows; i++) {
                    RowRollUp();
                    FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int i = 0; i < 4; i++) {

                for (ushort c = 0; c < Columns; c++) {
                    FrameRollLeft();
                    FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int c = 0; c < 4; c++) {
                for (int i = 0; i < Rows; i++) {
                    RowRollDown();
                    FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int j = 0; j < 4; j++) {
                for (int i = 0; i < Rows; i++) {
                    ColumnRollLeft(0);
                    ColumnRollRight(1);
                    ColumnRollLeft(2);
                    ColumnRollRight(3);
                    ColumnRollLeft(4);
                    ColumnRollRight(5);
                    ColumnRollLeft(6);
                    ColumnRollRight(7);
                    FrameDraw();
                    Thread.Sleep(100);
                }
                Thread.Sleep(500);
            }
        }

        private void Random() {
            for (int j = 0; j < 5; j++) {
                for (int i = 0; i < 64; i++) {
                    FrameSet(i, true);
                    FrameSet((63 - i), true);
                    FrameDraw();
                    Thread.Sleep(15);
                    FrameSet(i, false);
                    FrameSet((63 - i), false);
                    FrameDraw();
                    Thread.Sleep(15);
                }
            }
        }

        private void RunSequence() {

            FrameSetBrightness(4);
            FrameSetBlinkRate(BlinkRate.Off);

            while (true) {
                for (int i = 0; i < cycles.Length; i++) {
                    ExecuteCycle(cycles[i]);
                }
            }
        }
    }
}

