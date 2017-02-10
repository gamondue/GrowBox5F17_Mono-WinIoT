using System;
using Raspberry.IO.GeneralPurpose;
using GrowBoxShared;
using System.Threading;

namespace Test_DigitalMonoIO
{

    // !!!! la classe DigitalIO, già fatta dal prof., andava USATA con questo programma, non RICOPIATA QUI !!!!!!!!!!!!!!!!!!!!!!!!!!
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // MONTI

    //class DigitalIO
    //{
    //    public ConnectorPin connectorPin;
    //    public ProcessorPin processorPin;

    //    IGpioConnectionDriver driver = GpioConnectionSettings.DefaultDriver;

    //    /// <summary>
    //    /// Costruttore che fa uso della classe ConnectorPin della libreria
    //    /// Da non usare se si vuole mantenere il programma portabile fra 
    //    /// Win 10 Iot e 
    //    /// </summary>
    //    /// <param name="ConnectorPin">
    //    /// Oggetto ConnectorPin usato per questo IO digitale
    //    /// </param>
    //    /// <param name="IsInput">
    //    /// Direzione dell'IO: se true è un Input, altrimenti è un output
    //    /// </param>
    //    public DigitalIO(ConnectorPin ConnectorPin, GpioPinDriveMode DriveMode)
    //    {
    //        connectorPin = (ConnectorPin)ConnectorPin;
    //        processorPin = connectorPin.ToProcessor();
    //        PinDirection dir;
    //        if (DriveMode == GpioPinDriveMode.Input)
    //            dir = PinDirection.Input;
    //        else if (DriveMode == GpioPinDriveMode.Output)
    //            dir = PinDirection.Output;
    //        else
    //            throw new NotImplementedException("Drive mode dell'I/O impossibile con Mono");

    //        driver.Allocate(processorPin, dir);
    //    }
    //    /// <summary>
    //    /// Costruttore che permette di indicare il canale da usare come integer.
    //    /// Usa un trucco, perchè la libreria non supporta l'uso di un integer
    //    /// per istanziare la classe di controllo dell'IO digitale
    //    /// </summary>
    //    /// <param name="NumConnectorPin">
    //    /// Numero del canale 
    //    /// E' il numero del piedino nel connettore Gpio del Raspberry Pi, NON quello 
    //    /// del SoC Broadcom
    //    /// </param>
    //    /// <param name="IsInput">
    //    /// Direzione dell'IO: se true è un Input, altrimenti è un output
    //    /// </param>
    //    public DigitalIO(int NumConnectorPin, GpioPinDriveMode DriveMode)
    //    {
    //        foreach (ConnectorPin pin in Enum.GetValues(typeof(ConnectorPin)))
    //        {
    //            try
    //            {
    //                if (Enum.GetName(typeof(ConnectorPin),
    //                    pin).ToString().IndexOf(NumConnectorPin.ToString(), 0) >= 0)
    //                {
    //                    connectorPin = pin;
    //                    processorPin = connectorPin.ToProcessor();
    //                    Console.WriteLine("Numero pin: {0} Definizione pin:{1}",
    //                        NumConnectorPin, Enum.GetName(typeof(ConnectorPin), pin).ToString());
    //                }
    //                PinDirection dir;
    //                if (DriveMode == GpioPinDriveMode.Input)
    //                    dir = PinDirection.Input;
    //                else if (DriveMode == GpioPinDriveMode.Output)
    //                    dir = PinDirection.Output;
    //                else
    //                    throw new NotImplementedException("Drive mode dell'I/O non ancora possibile con Mono");

    //                driver.Allocate(processorPin, dir);
    //            }
    //            catch
    //            { // se quel pin non c'è nel Raspberry che uso, dà errore
    //            }
    //        }
    //    }
    //    public GpioPinValue Read()
    //    {
    //        if (driver.Read(processorPin))
    //            return GpioPinValue.High;
    //        else
    //            return GpioPinValue.Low;
    //    }

    //    public void Write(GpioPinValue value)
    //    {
    //        if (value == GpioPinValue.High)
    //            driver.Write(processorPin, true);
    //        else
    //            driver.Write(processorPin, false);
    //    }

    //    public void Wait(bool waitForUp, System.TimeSpan Timeout)
    //    {
    //        driver.Wait(processorPin, waitForUp, Timeout);
    //    }

    //}
    ///// <summary>
    ///// Enum per il modo di controllo di un IO digitale
    ///// Enum uguale a quello di Win 10 IoT, per migliore compatibilità
    ///// </summary>
    //public enum GpioPinDriveMode
    //{
    //    Input,
    //    InputPullDown,
    //    InputPullUp,
    //    Output,
    //    OutputOpenDrain,
    //    OutputOpenDrainPullUp,
    //    OutputOpenSource,
    //    OutputOpenSourcePullDown,
    //}
    ///// <summary>
    ///// Enum per i valori ammessi in un IO digitale
    ///// Enum uguale a quello di Win 10 IoT, per migliore compatibilità
    ///// </summary>
    //public enum GpioPinValue
    //{
    //    High,
    //    Low
    //}
}