
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Globalization



Public Class frmUniversalPacking


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

        UpdateImageValues()

        updatePackGrid()




    End Sub

    Private Sub UpdateImageValues()

        'This section will check machine number and P1 and apply correct drum numbers on screen


        Select Case frmJobEntry.varCartNum
            Case "P1"
                'Put new Drum numbers on images

                Me.Controls("btn1").Text = "01"
                Me.Controls("btn2").Text = "02"
                Me.Controls("btn3").Text = "03"
                Me.Controls("btn4").Text = "04"
                Me.Controls("btn5").Text = "09"
                Me.Controls("btn6").Text = "10"
                Me.Controls("btn7").Text = "11"
                Me.Controls("btn8").Text = "12"
                Me.Controls("btn9").Text = "17"
                Me.Controls("btn10").Text = "18"
                Me.Controls("btn11").Text = "19"
                Me.Controls("btn12").Text = "20"
                Me.Controls("btn13").Text = "25"
                Me.Controls("btn14").Text = "26"
                Me.Controls("btn15").Text = "27"
                    Me.Controls("btn16").Text = "28"

            Case "P2"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "05"
                Me.Controls("btn2").Text = "06"
                Me.Controls("btn3").Text = "07"
                Me.Controls("btn4").Text = "08"
                Me.Controls("btn5").Text = "13"
                Me.Controls("btn6").Text = "14"
                Me.Controls("btn7").Text = "15"
                Me.Controls("btn8").Text = "16"
                Me.Controls("btn9").Text = "21"
                Me.Controls("btn10").Text = "22"
                Me.Controls("btn11").Text = "23"
                Me.Controls("btn12").Text = "24"
                Me.Controls("btn13").Text = "29"
                Me.Controls("btn14").Text = "30"
                Me.Controls("btn15").Text = "31"
                Me.Controls("btn16").Text = "32"

            Case "P5"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "33"
                Me.Controls("btn2").Text = "34"
                Me.Controls("btn3").Text = "35"
                Me.Controls("btn4").Text = "36"
                Me.Controls("btn5").Text = "41"
                Me.Controls("btn6").Text = "42"
                Me.Controls("btn7").Text = "43"
                Me.Controls("btn8").Text = "44"
                Me.Controls("btn9").Text = "49"
                Me.Controls("btn10").Text = "50"
                Me.Controls("btn11").Text = "51"
                Me.Controls("btn12").Text = "52"
                Me.Controls("btn13").Text = "57"
                Me.Controls("btn14").Text = "58"
                Me.Controls("btn15").Text = "59"
                Me.Controls("btn16").Text = "60"

            Case "P6"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "37"
                Me.Controls("btn2").Text = "38"
                Me.Controls("btn3").Text = "39"
                Me.Controls("btn4").Text = "40"
                Me.Controls("btn5").Text = "45"
                Me.Controls("btn6").Text = "46"
                Me.Controls("btn7").Text = "47"
                Me.Controls("btn8").Text = "48"
                Me.Controls("btn9").Text = "53"
                Me.Controls("btn10").Text = "54"
                Me.Controls("btn11").Text = "55"
                Me.Controls("btn12").Text = "56"
                Me.Controls("btn13").Text = "61"
                Me.Controls("btn14").Text = "62"
                Me.Controls("btn15").Text = "63"
                Me.Controls("btn16").Text = "64"
        End Select

        UpdateDrumVal()

    End Sub

    Private Sub UpdateDrumVal()

        Dim cellVal As String
        Dim reasonFound As Integer = 0


        Try
            For rw As Integer = 1 To 16 '16 Drum on each cart


                'Update Scanned Image
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value) Then

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value.ToString
                        'CHECK FOR SCANNED Drum AND SET TO GREEN
                        If cellVal = 3 Then
                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.Have_Drum
                            Me.Controls("btn" & rw).Enabled = True
                        ElseIf cellVal = 15 Then
                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.Packed_Drum
                            Me.Controls("btn" & rw).Enabled = False
                        End If
                        cellVal = Nothing
                    End If




                'CHECK FOR SHORT AND UPDATE IMAGE
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then

                    If frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value > 0 Then
                        cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value

                        If cellVal = 3 Then

                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortDrum
                            Me.Controls("btn" & rw).Enabled = True
                            shortC(rw) = 1
                        End If

                        If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                            If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value > 0 Then
                                Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortWithDefect
                                Me.Controls("btn" & rw).Enabled = True
                                shortC(rw) = 1
                            End If
                        End If

                        cellVal = Nothing

                    End If
                End If


                'CHECK FOR MISSING DRUM AND UPDATE IMAGE
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value) Then

                    If frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value > 0 Then
                        cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value
                        If cellVal > 0 Then

                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.NoDrum
                            Me.Controls("btn" & rw).Enabled = False

                            cellVal = Nothing
                        End If
                    End If
                End If

                'CHECK FOR DEFECT AND UPDATE IMAGE
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value

                    If cellVal > 0 Then

                        Me.Controls("btn" & rw).BackgroundImage = My.Resources.DefectDrum

                        If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then
                            If frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value > 0 Then
                                Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortWithDefect
                            End If
                        End If

                        cellVal = Nothing
                    End If

                End If

            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub updatePackGrid()

        Dim tmpPalletDrums = frmJobEntry.drumPerPal

        Select Case tmpPalletDrums

            Case 48
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 9 - 20 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 29 - 40 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 49 - 60 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 69 - 80 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 89 - 100 'hide
                            Me.Controls("btn" & i).Visible = False
                        Case 109 - 120 'hide
                            Me.Controls("btn" & i).Visible = False
                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 108
                    Select Case i
                        Case 1 - 8
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 - 28 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 - 48 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 - 68 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 - 88 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 - 108 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

            Case 72
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 16 - 20 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 36 - 40 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 53 - 60 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 73 - 80 'Hide
                            Me.Controls("btn" & i).Visible = False
                        Case 93 - 100 'hide
                            Me.Controls("btn" & i).Visible = False
                        Case 113 - 120 'hide
                            Me.Controls("btn" & i).Visible = False
                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 108
                    Select Case i
                        Case 1 - 12
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 - 32 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 - 52 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 - 72 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 - 92 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 - 112 'show and re number
                            Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

        End Select

    End Sub




End Class