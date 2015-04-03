using System;
using Raspberry.IO.InterIntegratedCircuit;
using System.Threading;

namespace Glovebox.RaspberryPi.Actuators.AdaFruit8x8Matrix {
    public class AdaFruit8x8Matrix : Matrix8x8, IDisposable {
        #region IDisposable implementation

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        #endregion

        Thread matrix;

        public AdaFruit8x8Matrix(I2cDriver i2cDriver)
            : base(i2cDriver.Connect(0x70)) {
            matrix = new Thread(new ThreadStart(this.RunSequence));
            matrix.Start();
        }

        private void RunSequence() {

            FrameSetBrightness(6);
            FrameSetBlinkRate(BlinkRate.Off);


            while (true) {

                //   DrawString("hello world", 100);


                for (int i = 0; i < fontSimple.Length; i++) {
                    DrawBitmap(fontSimple[i]);
                    FrameDraw();
                    Thread.Sleep(100);
                }

                foreach (Symbols suit in Enum.GetValues(typeof(Symbols))) {
                    DrawSymbol(suit);
                    FrameDraw();
                    Thread.Sleep(250);
                }

                DrawSymbol(Symbols.Heart);
                FrameDraw();
                Thread.Sleep(50);

                for (int i = 0; i < 4; i++) {
                    for (ushort c = 0; c < Columns; c++) {
                        ColumnRollRight(c);
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
                        ColumnRollLeft(c);
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

                for (int j = 0; j < 5; j++) {
                    for (int i = 0; i < 64; i++) {
                        FrameSet(i, true);
                        FrameSet((63 - i), true);
                        FrameDraw();
                        Thread.Sleep(10);
                        FrameSet(i, false);
                        FrameSet((63 - i), false);
                        FrameDraw();
                        Thread.Sleep(10);
                    }
                }
            }
        }
    }
}

