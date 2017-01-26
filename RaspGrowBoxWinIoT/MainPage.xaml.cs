# define WINIOT

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Gpio;
using GrowBoxShared;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace AppRaspGrowBox
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 16;
        private GpioPin _pin;
        private GpioPinValue _pinValue;
        private DispatcherTimer _timer;
        private DigitalConverterMCP3208 adc;
        private LightSensor lux;
        private TemperatureSensor temp;
        private SoilMoistureSensor moisture;
        private CO2Sensor co2;
        private RelativeHumiditySensor rh;
        private OutShiftRegister shift;
        private LCD1602Shift lcd;
        private FileLogger fileLog;
        private Device_OnOffShift irrigator;
        private Device_OnOffShift humidifier;
        private LightDevice lightDevice;

        public MainPage()
        {


            //fileLog = new FileLogger("file.log");

            // Crea ed utilizza un ADC. Il codice del ADC utilizzerà un gestore del bus SPI
            adc = new DigitalConverterMCP3208();

            lux = new LightSensor(adc, 0, 3.3);
            temp = new TemperatureSensor(adc, 1, 3.3);
            moisture = new SoilMoistureSensor(adc, 2, 3.3);
            co2 = new CO2Sensor(adc, 3, 4.9);
            rh = new RelativeHumiditySensor(adc, 4, 5.0);

            shift = new OutShiftRegister(16, 25, 24, 23);

            lcd = new LCD1602Shift(shift, 12, 11, 10, 9, 13, 14);

            irrigator = new Device_OnOffShift(shift, 8);
            //irrigator.WriteLog += WriteFileLog;
            humidifier = new Device_OnOffShift(shift, 2);
            //humidifier.WriteLog += WriteFileLog;

            //lightDevice = new LightDevice(shift, 3, 4, 5, 6, 7, 21, 20);
            //lightDevice.PercentualeApertura(50);

            //irrigator.TimeOn(3000);
              this.InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += Timer_Tick;
            InitGPIO();
            ////////////if (_pin != null)
            ////////////{
            _timer.Start();
            //////////}


          

        }


        private void WriteFileLog(string Line)
        {
            fileLog.WriteLogger(Line);
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                _pin = null;
                return;
            }
            _pin = gpio.OpenPin(LED_PIN);
            _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
        }


        private void Timer_Tick(object sender, object e)
        {
            if (_pinValue == GpioPinValue.High) _pinValue = GpioPinValue.Low;
            else _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);

            textLux.Text = lux.Read().ToString("F0");
            textGradi.Text = temp.Read().ToString("F1");
            textRH.Text = rh.Read().ToString("F0");
            textPPM.Text = co2.Read().ToString("F0");
            textUT.Text = moisture.Read().ToString("F0");
            textVoltmetro.Text = adc.ReadVolt(5).ToString("F1");

        }




    }
}
