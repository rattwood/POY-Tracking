<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraceEntry
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtTraceNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTmpTraceNum = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lblError = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtTraceNum
        '
        Me.txtTraceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTraceNum.Location = New System.Drawing.Point(301, 161)
        Me.txtTraceNum.Name = "txtTraceNum"
        Me.txtTraceNum.Size = New System.Drawing.Size(176, 38)
        Me.txtTraceNum.TabIndex = 0
        Me.txtTraceNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 164)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(261, 31)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Scan Trace Barcode"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 202)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Please note this Temp Trace number"
        '
        'lblTmpTraceNum
        '
        Me.lblTmpTraceNum.AutoSize = True
        Me.lblTmpTraceNum.Location = New System.Drawing.Point(32, 215)
        Me.lblTmpTraceNum.Name = "lblTmpTraceNum"
        Me.lblTmpTraceNum.Size = New System.Drawing.Size(106, 13)
        Me.lblTmpTraceNum.TabIndex = 3
        Me.lblTmpTraceNum.Text = "Scan Trace Barcode"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(12, 314)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(111, 57)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(495, 154)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(151, 61)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        Me.btnUpdate.Visible = False
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.BackColor = System.Drawing.Color.Red
        Me.lblError.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblError.Location = New System.Drawing.Point(12, 20)
        Me.lblError.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(126, 39)
        Me.lblError.TabIndex = 332
        Me.lblError.Text = "Label3"
        Me.lblError.Visible = False
        '
        'frmTraceEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(658, 379)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblTmpTraceNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTraceNum)
        Me.Name = "frmTraceEntry"
        Me.Text = "Trace Entry"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtTraceNum As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblTmpTraceNum As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents lblError As Label
End Class
