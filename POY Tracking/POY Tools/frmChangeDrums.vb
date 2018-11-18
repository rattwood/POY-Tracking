Imports System.Data.SqlClient

Public Class frmChangeDrums
    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
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



    Dim count As Integer = 0




    Private Sub frmChangeDrums_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadDGV()


        Me.KeyPreview = True  'Allows us to look for advace character from barcode


    End Sub

    'Private Sub updateDGV()

    '    Try
    '        For i = 1 To frmDGV.DGVdata.Rows.Count
    '            If IsDBNull(DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value) Then
    '                Continue For
    '            End If

    '            'write the new value of drum in to db
    '            frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value = DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value
    '            frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKDATE").Value = frmJobEntry.time  'Update update time
    '            MsgBox("test")
    '        Next

    '    Catch ex As Exception
    '        MsgBox("DGV Update Error " & vbNewLine & ex.Message)

    '    End Try

    '    Close()
    '    frmToolEntry.Show()


    'End Sub

    Private Sub loadDGV()

        For i = 1 To frmDGV.DGVdata.Rows.Count
            If Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value) Then
                count = count + 1
            End If
        Next

        'SET SIZE OF DGV FORM
        DGVChageDrum.Rows.Add(count)

        For i = 1 To frmDGV.DGVdata.Rows.Count
            DGVChageDrum.Rows(i - 1).Cells("POYPACKIDX").Value = frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value
            DGVChageDrum.Rows(i - 1).Cells("POYSTEPNUM").Value = frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value
            DGVChageDrum.Rows(i - 1).Cells("POYBCODEDRUM").Value = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value
            DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value = ""
        Next


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

            LException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub



    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        For i = 1 To frmDGV.DGVdata.Rows.Count
            DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value = Nothing
        Next
        btnUpdate.Enabled = False

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
        frmToolEntry.Show()
    End Sub

    Private Sub entryChk()
        Dim errorChk As String
        Dim prodnum As String = frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value.ToString


        For i = 1 To frmDGV.DGVdata.Rows.Count
            If DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value = Nothing Then
                Continue For
            ElseIf DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value.ToString = "" Then
                Continue For
            End If

            errorChk = DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value.ToString

                If Not (errorChk.Length = 14) Then
                    MsgBox("Index No. " & i & " Replacment Drum " & vbCrLf & "barcode " & errorChk & " is wrong")
                    Exit Sub
                ElseIf Not (errorChk.Substring(2, 3).Equals(prodnum)) Then
                MsgBox("Index No. " & i & " Replacment Drum " & errorChk & " is wrong product code")
                Exit Sub
            End If



                LExecQuery("Select * from POYTrack where POYBCODEDRUM = '" & errorChk & "'ORDER BY POYPACKIDX")

                If LRecordCount > 0 Then
                    MsgBox("Replacment Drum " & errorChk & " is already used")
                    Exit Sub
                End If
            Next

        btnUpdate.Enabled = True


    End Sub




    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        entryChk()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        entryChk()

        Try
            For i = 1 To frmDGV.DGVdata.Rows.Count
                If DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value = Nothing Then
                    Continue For
                ElseIf DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value.ToString = "" Then
                    Continue For
                End If

                'write the new value of drum in to db
                frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value = DGVChageDrum.Rows(i - 1).Cells("POYREPBCODEDRUM").Value
                frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKDATE").Value = frmJobEntry.time  'Update update time

            Next

        Catch ex As Exception
            MsgBox("DGV Update Error " & vbNewLine & ex.Message)

        End Try

        Close()
        frmToolEntry.UpdateDatabase()
        frmToolEntry.chkPackingExists()
        frmToolEntry.smalldbUpdate()
        frmToolEntry.Show()

    End Sub


    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmTraceEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then
            entryChk()
        End If

    End Sub


End Class