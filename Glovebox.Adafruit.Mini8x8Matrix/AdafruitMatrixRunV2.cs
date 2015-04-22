using Raspberry.IO.InterIntegratedCircuit;
using System;
using System.Threading;
using Glovebox.Adafruit.Mini8x8Matrix;
using Glovebox.IoT;

namespace Glovebox.Adafruit.Mini8x8Matrix {
    public class AdaFruitMatrixRunV2 : IDisposable {

        Thread matrixThread;
        Adafruit8x8Matrix matrix;

        public AdaFruitMatrixRunV2(Adafruit8x8Matrix matrix)  {
            this.matrix = matrix;

            CreateCyclesCollection();

            matrixThread = new Thread(new ThreadStart(this.RunSequence));
            matrixThread.Start();
        }

        private void CreateCyclesCollection() {
            matrix.cycles = new DoCycle[] {
            new DoCycle(MyIPAddress),
          //  new DoCycle(HappyBirthday),
            new DoCycle(AlphaNumeric),
            new DoCycle(ShowSymbols),
            new DoCycle(Hearts),
            new DoCycle(Random),
            };
        }

        private void MyIPAddress() {
            matrix.ScrollStringInFromRight("IP: " + Utilities.GetIPAddress(), 100);
            Thread.Sleep(200);
        }

        private void HappyBirthday() {
            matrix.ScrollStringInFromRight("Happy Birthday", 100);
            matrix.ScrollSymbolInFromRight(new Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols[] { Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols.Heart, Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols.Heart }, 100);
        }

        private void AlphaNumeric() {
            matrix.DrawFontSet(100);
        }

        private void ShowSymbols() {
            foreach (Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols suit in Enum.GetValues(typeof(Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols))) {
                matrix.DrawSymbol(suit);
                matrix.FrameDraw();
                Thread.Sleep(100);
            }
        }

        private void Hearts() {
            matrix.DrawSymbol(Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.Symbols.Heart);
            matrix.FrameDraw();
            Thread.Sleep(50);

            for (int i = 0; i < 2; i++) {
                for (ushort c = 0; c < matrix.Columns; c++) {
                    matrix.FrameRollRight();
                    matrix.FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int c = 0; c < 2; c++) {
                for (int i = 0; i < matrix.Rows; i++) {
                    matrix.RowRollUp();
                    matrix.FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int i = 0; i < 2; i++) {

                for (ushort c = 0; c < matrix.Columns; c++) {
                    matrix.FrameRollLeft();
                    matrix.FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int c = 0; c < 2; c++) {
                for (int i = 0; i < matrix.Rows; i++) {
                    matrix.RowRollDown();
                    matrix.FrameDraw();
                    Thread.Sleep(50);
                }
            }

            for (int j = 0; j < 2; j++) {
                for (int i = 0; i < matrix.Rows; i++) {
                    matrix.ColumnRollLeft(0);
                    matrix.ColumnRollRight(1);
                    matrix.ColumnRollLeft(2);
                    matrix.ColumnRollRight(3);
                    matrix.ColumnRollLeft(4);
                    matrix.ColumnRollRight(5);
                    matrix.ColumnRollLeft(6);
                    matrix.ColumnRollRight(7);
                    matrix.FrameDraw();
                    Thread.Sleep(100);
                }
                Thread.Sleep(500);
            }
        }

        private void Random() {
            for (int j = 0; j < 2; j++) {
                for (int i = 0; i < 64; i++) {
                    matrix.FrameSet(i, true);
                    matrix.FrameSet((63 - i), true);
                    matrix.FrameDraw();
                    Thread.Sleep(15);
                    matrix.FrameSet(i, false);
                    matrix.FrameSet((63 - i), false);
                    matrix.FrameDraw();
                    Thread.Sleep(15);
                }
            }
        }

        private void RunSequence() {

            matrix.FrameSetBrightness(4);
            matrix.FrameSetBlinkRate(Glovebox.Adafruit.Mini8x8Matrix.Adafruit8x8Matrix.BlinkRate.Off);

            while (true) {
                for (int i = 0; i < matrix.cycles.Length; i++) {
                    matrix.ExecuteCycle(matrix.cycles[i]);
                }
            }
        }

        public void Dispose() {
        }
    }
}

