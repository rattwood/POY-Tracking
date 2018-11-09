Imports System.IO

Public Class frmPackRepMain

    'STRINGS
    Dim prodNameMod As String
    Dim saveString As String
    Dim yestname1 As String
    Dim yestname2 As String
    Dim yestname3 As String

    Public prevDays As String
    Public sheetName As String
    Public savename As String
    Public template As String
    Public prevDaysName As String

    'DIRECTORY PATHS ALL PUBLIC
    Public finPath As String
    Dim todayPath As String
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String



    Public Sub PackRepMainSub()



        If frmJobEntry.drumPerPal = "48" Then

            'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
            prodNameMod = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString
            prodNameMod = prodNameMod.Replace("/", "_")

            'CREATE THE SHEET NAME But as this Cheese is from ReCheck we will assign to A grade sheet
            sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_48"



            'CREATE THE FULL NAME FOR SAVING THE FILE
            'saveString = (prodNameMod & " " _
            '    & frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value.ToString & "_" _
            '    & frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value.ToString) & " 48"
            saveString = frmTraceEntry.bcodeScan


        ElseIf frmJobEntry.drumPerPal = "72" Then
            'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
            prodNameMod = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString
            prodNameMod = prodNameMod.Replace("/", "_")

            'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
            sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_72"
            'CREATE THE FULL NAME FOR SAVING THE FILE
            saveString = (prodNameMod & " " _
                & frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value.ToString & "_" _
                & frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value.ToString) & " 72"

        ElseIf frmJobEntry.drumPerPal = "120" Then
            'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
            prodNameMod = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString
            prodNameMod = prodNameMod.Replace("/", "_")

            'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
            sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_120"

            'CREATE THE FULL NAME FOR SAVING THE FILE
            saveString = (prodNameMod & " " _
                & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                & frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value.ToString) & " 120"
        End If




        'CALL SUB TO GET TODAYS SAVE DIRECTORY
        todayDir()



        'create the save name of the file
        savename = (todayPath & "\" & saveString & ".xlsx").ToString


        'SELECT CORRECT PRINT TEMPLATE

        Select Case frmJobEntry.drumPerPal

            Case "48"
                template = (My.Settings.dirTemplate & "\" & "tmpTraceDrumPerPall.xlsx").ToString
            Case "72"
                template = (My.Settings.dirTemplate & "\" & "tmpTraceDrumPerPall.xlsx").ToString
            Case "120"
                template = (My.Settings.dirTemplate & "\" & "tmpTraceDrumPerPall.xlsx").ToString
        End Select



        'Create PREVIOUS THREE DAYS CHECK NAMES
        yestname1 = (PrevPath1 & "\" & saveString & ".xlsx").ToString
        yestname2 = (PrevPath2 & "\" & saveString & ".xlsx").ToString
        yestname3 = (PrevPath3 & "\" & saveString & ".xlsx").ToString

        'CHECK TO SEE IF THE TEMPLATE DIRECTORY HAS A REFRENCE OTHERWISE QUIT
        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Me.Close()
            frmJobEntry.Show()
        End If


        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        If File.Exists(savename) Then

            Select Case frmJobEntry.drumPerPal


                Case "48"
                    frmPackTodayUpdate.TodatUpdate48()
                    frmPacking48.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen
                Case "72"
                    frmPackTodayUpdate.TodayUpdate72()
                Case "120"
                    frmPackTodayUpdate.TodayUpdate120()


            End Select

            frmPackTodayUpdate.Close()
            frmPacking48.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen

            Exit Sub


        Else


            'If File.Exists(yestname1) Then      'ONE DAY AGO
            '    prevDaysName = yestname1
            '    prevDays = Date.Now.AddDays(-1).ToString("ddMMyyyy")
            '    frmPackPrvGet.PrvGet()
            '    Me.Close()
            'ElseIf File.Exists(yestname2) Then  'TWO DAYS AGO
            '    prevDaysName = yestname2
            '    prevDays = Date.Now.AddDays(-2).ToString("ddMMyyyy")
            '    frmPackPrvGet.PrvGet()
            '    Me.Close()
            'ElseIf File.Exists(yestname3) Then  'THREE DAYS AGO
            '    prevDaysName = yestname3
            '    prevDays = Date.Now.AddDays(-3).ToString("ddMMyyyy")
            '    frmPackPrvGet.PrvGet()
            '    Me.Close()
            'Else
            frmPackCreateNew.CreateNew()
                Me.Close()
            'End If
        End If







    End Sub

    'SUBROUTINE TO CHECK IF DAY DIRECTORIES EXIST IF NOT THEY ARE CREATED
    Private Sub todayDir()

        ' routine to check if a today directory exists otherwise creat a new one
        PrevPath1 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
        PrevPath2 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-2).ToString("dd_MM_yyyy"))
        PrevPath3 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-3).ToString("dd_MM_yyyy"))


        todayPath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))

        If Not Directory.Exists(todayPath) Then
            Directory.CreateDirectory(todayPath)
        End If

        If Not Directory.Exists(finPath) Then
            Directory.CreateDirectory(finPath)
        End If

    End Sub






    Private Sub frmPackRepMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class