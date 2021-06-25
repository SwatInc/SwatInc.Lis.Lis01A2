# SwatInc.Lis.Lis01A2
This library is an OO implementation of the CLSI LIS01-A2 standard. Specification for Low-Level Protocol to Transfer Messages Between Clinical Laboratory Instruments and 
Computer Systems.

The two libraries in this repo are C# implementations of the same from [Essy.LIS](https://www.nuget.org/packages/Essy.LIS.LIS02A2/).

## Instructions For Use


### TCP/IP
```C#
      var someIP = "192.168.1.11";
      UInt16 somePort = 1111;
      var lowLevelConnection = new Lis01A02TCPConnection(someIP, somePort);
      var lisConnection = new Lis01A2Connection(lowLevelConnection);
```
### Serial port
```C#
      var sp = new System.IO.Ports.SerialPort("COM1");
      // Config Serial port to your needs
      var lowLevelConnection = new Lis01A02RS232Connection(sp);
      var lisConnection = new Lis01A2Connection(lowLevelConnection);
```

### Create LIS parser object and connect
```C#
      var LISParser = new LISParser(lisConnection);
      LISParser.OnSendProgress += LISParser_OnSendProgress; //Send data progress will trigger this event
      LISParser.OnReceivedRecord += LISParser_OnReceivedRecord; //incoming LIS frames will trigger this event
      LISParser.Connection.Connect();
```
```C#
    private static void LISParser_OnReceivedRecord(object Sender, ReceiveRecordEventArgs e)
    {

    }

    private static void LISParser_OnSendProgress(object sender, SendProgressEventArgs e)
    {

    }
```
### Now you are ready to receive incoming packets or you can transmit data
```C#
            var lisRecordList = new List<AbstractLisRecord>();
            var hr = new HeaderRecord();
            hr.SenderID = "Some Sender ID Code";
            hr.ProcessingID = HeaderProcessingID.Production;
            lisRecordList.Add(hr);
            var pr = new PatientRecord();
            pr.SequenceNumber = 1;
            pr.LaboratoryAssignedPatientID = "Sam001";
            lisRecordList.Add(pr);
            var orderRec = new OrderRecord();
            orderRec.SequenceNumber = 1;
            orderRec.SpecimenID = "Sam001";
            orderRec.TestID = new UniversalTestID();
            orderRec.TestID.ManufacturerCode = "T001";
            orderRec.ReportType = OrderReportType.Final;
            lisRecordList.Add(orderRec);
            pr = new PatientRecord();
            pr.SequenceNumber = 2;
            pr.LaboratoryAssignedPatientID = "Sam002";
            lisRecordList.Add(pr);
            orderRec = new OrderRecord();
            orderRec.SequenceNumber = 1;
            orderRec.SpecimenID = "Sam002";
            orderRec.TestID = new UniversalTestID();
            orderRec.TestID.ManufacturerCode = "T001";
            orderRec.ReportType = OrderReportType.Final;
            lisRecordList.Add(orderRec);
            var tr = new TerminatorRecord();
            lisRecordList.Add(tr);
            LISParser.SendRecords(lisRecordList);
```
You can fill the record data to your own needs. There are more record type that you can instantiate and transmit. The data is transmitted in the order of the list.


