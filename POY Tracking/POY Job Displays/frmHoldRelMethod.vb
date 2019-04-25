Public Class frmHoldRelMethod
    Public varRelGrade As String
    Public varHoldrelOP As String



    Private Sub frmHoldRelMethod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnGradeAB.BackColor = Color.Gray
        btnGradeA.BackColor = Color.Gray
        btnWaste.BackColor = Color.Gray


    End Sub

    Private Sub txtBoxOpName_TextChanged(sender As Object, e As EventArgs) Handles txtBoxOpName.TextChanged

        btnGradeAB.Enabled = True
        btnGradeA.Enabled = True
        btnWaste.Enabled = True
        btnGradeAB.BackColor = Color.Yellow
        btnGradeA.BackColor = Color.YellowGreen
        btnWaste.BackColor = Color.Violet




        btnOK.Visible = False
    End Sub






    Private Sub btnGradeAB_Click(sender As Object, e As EventArgs) Handles btnGradeAB.Click

        btnGradeAB.Enabled = True
        btnGradeA.Enabled = False
        btnWaste.Enabled = False
        btnGradeAB.BackColor = Color.Yellow
        btnGradeA.BackColor = Color.Gray
        btnWaste.BackColor = Color.Gray

        btnChangeSel.Visible = True
        btnOK.Visible = True

        varRelGrade = "AB"
        varHoldrelOP = txtBoxOpName.Text

    End Sub

    Private Sub btnGradeA_Click(sender As Object, e As EventArgs) Handles btnGradeA.Click

        btnGradeAB.Enabled = False
        btnGradeA.Enabled = True
        btnWaste.Enabled = False
        btnGradeAB.BackColor = Color.Gray
        btnGradeA.BackColor = Color.YellowGreen
        btnWaste.BackColor = Color.Gray

        btnChangeSel.Visible = True
        btnOK.Visible = True

        varRelGrade = "A"
        varHoldrelOP = txtBoxOpName.Text
    End Sub

    Private Sub btnWaste_Click(sender As Object, e As EventArgs) Handles btnWaste.Click

        btnGradeAB.Enabled = False
        btnGradeA.Enabled = False
        btnWaste.Enabled = True
        btnGradeAB.BackColor = Color.Gray
        btnGradeA.BackColor = Color.Gray
        btnWaste.BackColor = Color.Violet

        btnChangeSel.Visible = True
        btnOK.Visible = True
        varRelGrade = "WASTE"
        varHoldrelOP = txtBoxOpName.Text

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Hide()
        frmSelectRelease.txtBoxDrumBcode.Focus()
        frmSelectRelease.Show()
    End Sub

    Private Sub btnChangeSel_Click(sender As Object, e As EventArgs) Handles btnChangeSel.Click
        btnGradeAB.Enabled = True
        btnGradeA.Enabled = True
        btnWaste.Enabled = True

        btnGradeAB.BackColor = Color.Yellow
        btnGradeA.BackColor = Color.YellowGreen
        btnWaste.BackColor = Color.Violet

        btnChangeSel.Visible = False
        btnOK.Visible = False

        varRelGrade = ""


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmJobEntry.Show()
    End Sub


End Class