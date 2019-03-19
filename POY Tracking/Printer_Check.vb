
Imports System
Imports System.Management


Public Class Printer_Check


    Class PrinterOffline
        <STAThread()>
        Private Shared Sub Main(ByVal args As String())
            ' Set management scope 
            Dim scope As New ManagementScope("\root\cimv2")
            scope.Connect()
            ' Select Printers from WMI Object Collections 
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            Dim printerName As String = ""
            For Each printer As ManagementObject In searcher.[Get]()
                printerName = printer("Name").ToString().ToLower()
                If printerName.Equals("hp deskjet 930c") Then
                    Console.WriteLine("Printer = " + printer("Name"))
                    If printer("WorkOffline").ToString().ToLower().Equals("true") Then
                        ' printer is offline by user 
                        Console.WriteLine("Your Plug-N-Play printer is not connected.")
                    Else
                        ' printer is not offline 
                        Console.WriteLine("Your Plug-N-Play printer is connected.")
                    End If
                End If
            Next
        End Sub
    End Class






End Class
