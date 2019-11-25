
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Globalization
Imports System.Data.SqlClient


Public Class frmUniversalPacking
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    ' Private SQL As New SQLConn
    Private writeerrorLog As New writeError


    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------

    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    ' Public LConn As New SqlConnection("Server=192.168.1.211,1433;Database=Toraydb;User ID=sa;Password=tecknose4260")

    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public LDA As SqlDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SqlCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SqlParameter)
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    'TIME
    Dim time As New DateTime
    Public todayTimeDate As String
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"


    'Manual assesment variables
    Dim btnImage As Image
    Dim keepDefcodes As Integer

    'Faults
    Dim Fault_S As String
    Dim Fault_X As String
    Dim shortC(16) As String
    Dim bcodeScan As String
    Private rowendcount As Integer
    Private allocatedCount As Integer 'count of DRUMs scanned
    Private toAllocateCount As Integer
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Dim saveB As Integer
    Dim saveS As Integer
    Public NoDRUM As Integer

    Public defect As Integer
    Public POYSHORTDRUM As Integer
    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Public DRUMNumOffset As Integer
    Dim varDRUMBCode As String
    Dim fileActive As Integer
    Public varDRUMNum As Integer

    Public DRUMCount As Integer
    ' Public DRUMState As String
    Dim fltDRUMNum As String




    Private Sub frmUniversalPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        lblCartNum.Text = frmJobEntry.varCartNum



        'Check to see if this is existing Pallet or new Pallet


        UpdateImageValues()


    End Sub


    Private Sub UpdateImageValues()

        'This section will check machine number and P1 and apply correct drum numbers on screen


        Select Case frmJobEntry.varCartNum
            Case "P1"
                'Put new Drum numbers on images


                For I = 1 To 16
                    btn1.Text = "01"
                    btn2.Text = "02"
                    btn3.Text = "03"
                    btn4.Text = "04"
                    btn5.Text = "09"
                    btn6.Text = "10"
                    btn7.Text = "11"
                    btn8.Text = "12"
                    btn9.Text = "17"
                    btn10.Text = "18"
                    btn11.Text = "19"
                    btn12.Text = "20"
                    btn13.Text = "25"
                    btn14.Text = "26"
                    btn15.Text = "27"
                    btn16.Text = "28"
                Next
            Case "P2"
                'Put new Drum numbers on images
               btn1.Text = "05"
                btn2.Text = "06"
                btn3.Text = "07"
                btn4.Text = "08"
                btn5.Text = "13"
                btn6.Text = "14"
                btn7.Text = "15"
                btn8.Text = "16"
                btn9.Text = "21"
                btn10.Text = "22"
                btn11.Text = "23"
                btn12.Text = "24"
                btn13.Text = "29"
                btn14.Text = "30"
                btn15.Text = "31"
                btn16.Text = "32"

            Case "P5"
                'Put new Drum numbers on images
               btn1.Text = "33"
                btn2.Text = "34"
                btn3.Text = "35"
                btn4.Text = "36"
                btn5.Text = "41"
                btn6.Text = "42"
                btn7.Text = "43"
                btn8.Text = "44"
                btn9.Text = "49"
                btn10.Text = "50"
                btn11.Text = "51"
                btn12.Text = "52"
                btn13.Text = "57"
                btn14.Text = "58"
                btn15.Text = "59"
                btn16.Text = "60"

            Case "P6"
                'Put new Drum numbers on images
               btn1.Text = "37"
                btn2.Text = "38"
                btn3.Text = "39"
                btn4.Text = "40"
                btn5.Text = "45"
                btn6.Text = "46"
                btn7.Text = "47"
                btn8.Text = "48"
                btn9.Text = "53"
                btn10.Text = "54"
                btn11.Text = "55"
                btn12.Text = "56"
                btn13.Text = "61"
                btn14.Text = "62"
                btn15.Text = "63"
                btn16.Text = "64"
        End Select
        updatePackGrid()
        UpdateDrumVal()

    End Sub

    Private Sub UpdateDrumVal()

        Dim cellVal As String
        Dim reasonFound As Integer = 0
        Dim tmpCartNum As String

        MsgBox(frmJobEntry.varCartBCode)

        LExecQuery("Select POYBCODECART from POYTRACK where POYBCODEDRUM = '" & frmJobEntry.varCartBCode & "' ")
        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmCartDgv.DGVCart.DataSource = LDS.Tables(0)
            frmCartDgv.DGVCart.Rows(0).Selected = True
            tmpCartNum = frmCartDgv.DGVCart.Rows(0).Cells("POYBCODECART").Value
        End If




        LExecQuery("SELECT * FROM POYTrack WHERE POYBCODECART = '" & tmpCartNum & "'  ")
        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmCartDgv.DGVCart.DataSource = LDS.Tables(0)
            frmCartDgv.DGVCart.Rows(0).Selected = True

            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
        End If

        Try
            For rw As Integer = 1 To 16 '16 Drum on each cart


                'Update Scanned Image
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDRUMSTATE").Value) Then

                    cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDRUMSTATE").Value.ToString
                    'CHECK FOR SCANNED Drum AND SET TO GREEN
                    If cellVal = 3 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.Have_Drum
                        GroupBox4.Controls("btn" & rw).Enabled = True
                    ElseIf cellVal = 15 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.Packed_Drum
                        GroupBox4.Controls("btn" & rw).Enabled = False
                    End If
                    ' cellVal = Nothing
                End If




                'CHECK FOR SHORT AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then

                    If Val(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value) > 0 Then
                        cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value

                        If cellVal > 0 Then

                            GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                            GroupBox4.Controls("btn" & rw).Enabled = True

                        End If
                    End If
                End If


                'CHECK FOR MISSING DRUM AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value) Then

                    If frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value > 0 Then
                        cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value
                        If cellVal > 0 Then
                            GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                            GroupBox4.Controls("btn" & rw).Enabled = False
                        End If
                    End If
                End If

                'CHECK FOR DEFECT AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                    cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDEFDRUM").Value
                    If cellVal > 0 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                    End If
                End If
                cellVal = Nothing
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Public Sub LExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""


        If LConn.State = ConnectionState.Open Then LConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            LConn.Open()

            'CREATE SQL COMMAND
            LCmd = New SqlCommand(Query, LConn)

            'LOAD PARAMETER INTO SQL COMMAND
            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            LParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            LDS = New DataSet
            LDT = New DataTable
            LDA = New SqlDataAdapter(LCmd)

            LRecordCount = LDA.Fill(LDS)

        Catch ex As Exception

            LException = "ExecQuery Error:    " & vbNewLine & ex.Message
            MsgBox(LException)
            writeerrorLog.writelog("ExecQuery Error:", ex.Message, False, "System_Fault")
            writeerrorLog.writelog("ExecQuery Error:", ex.ToString, False, "System_Fault")

        End Try

    End Sub

    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub



    Private Sub updatePackGrid()

        Dim tmpPalletDrums = frmJobEntry.drumPerPal
        Dim tmpButton As Button




        'Select Case tmpPalletDrums

        Select Case frmJobEntry.drumPerPal

            Case 48
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 9 To 20 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 29 To 40 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 49 To 60 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 69 To 80 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 89 To 100 'hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 109 To 120 'hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False

                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 108
                    Select Case i
                        Case 1 To 8
                            GroupBox5.Controls("btnPacked" & i.ToString).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 To 28 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 To 48 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 To 68 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 To 88 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 To 108 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

            Case 72
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 13 To 20 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 33 To 40 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 53 To 60 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 73 To 80 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 93 To 100 'hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 113 To 120 'hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 112
                    Select Case i
                        Case 1 To 12
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 To 32 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 To 52 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 To 72 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 To 92 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 To 112 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

        End Select

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.cancelRoutine()
        frmJobEntry.Show()
        Me.Close()
    End Sub
End Class