<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgChangeUserPassword
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgChangeUserPassword))
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Simulate_Button = New System.Windows.Forms.Button()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.chkUpdateVariables = New System.Windows.Forms.CheckBox()
    Me.chkUpdateConfigurations = New System.Windows.Forms.CheckBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtOldPassword = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtOldPasswordRepeat = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.chkUpdateObjects = New System.Windows.Forms.CheckedListBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txtNewPassword = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.txtNewPasswordRepeat = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.txtUserID = New System.Windows.Forms.TextBox()
    Me.txtDomain = New System.Windows.Forms.TextBox()
    Me.chkUserIDFormats = New System.Windows.Forms.CheckedListBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lnkSelectAll = New System.Windows.Forms.LinkLabel()
    Me.lnkSelectNone = New System.Windows.Forms.LinkLabel()
    Me.lnkSelectInvert = New System.Windows.Forms.LinkLabel()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.workerPasswordChanger = New System.ComponentModel.BackgroundWorker()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 3
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.Simulate_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 2, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(376, 446)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(255, 29)
    Me.TableLayoutPanel1.TabIndex = 10
    '
    'Simulate_Button
    '
    Me.Simulate_Button.Location = New System.Drawing.Point(3, 3)
    Me.Simulate_Button.Name = "Simulate_Button"
    Me.Simulate_Button.Size = New System.Drawing.Size(67, 23)
    Me.Simulate_Button.TabIndex = 2
    Me.Simulate_Button.Text = "Simulate"
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(94, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(180, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Close"
    '
    'chkUpdateVariables
    '
    Me.chkUpdateVariables.AutoSize = True
    Me.chkUpdateVariables.Location = New System.Drawing.Point(18, 203)
    Me.chkUpdateVariables.Name = "chkUpdateVariables"
    Me.chkUpdateVariables.Size = New System.Drawing.Size(245, 17)
    Me.chkUpdateVariables.TabIndex = 5
    Me.chkUpdateVariables.Text = "Update Variables containing the Old Password"
    Me.chkUpdateVariables.UseVisualStyleBackColor = True
    '
    'chkUpdateConfigurations
    '
    Me.chkUpdateConfigurations.AutoSize = True
    Me.chkUpdateConfigurations.Location = New System.Drawing.Point(18, 226)
    Me.chkUpdateConfigurations.Name = "chkUpdateConfigurations"
    Me.chkUpdateConfigurations.Size = New System.Drawing.Size(269, 17)
    Me.chkUpdateConfigurations.TabIndex = 6
    Me.chkUpdateConfigurations.Text = "Update Configurations containing the Old Password"
    Me.chkUpdateConfigurations.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(359, 39)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(75, 13)
    Me.Label1.TabIndex = 13
    Me.Label1.Text = "Old Password:"
    '
    'txtOldPassword
    '
    Me.txtOldPassword.Enabled = False
    Me.txtOldPassword.Location = New System.Drawing.Point(441, 35)
    Me.txtOldPassword.Name = "txtOldPassword"
    Me.txtOldPassword.Size = New System.Drawing.Size(187, 20)
    Me.txtOldPassword.TabIndex = 3
    Me.txtOldPassword.UseSystemPasswordChar = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(321, 62)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(113, 13)
    Me.Label2.TabIndex = 15
    Me.Label2.Text = "Confirm Old Password:"
    '
    'txtOldPasswordRepeat
    '
    Me.txtOldPasswordRepeat.Enabled = False
    Me.txtOldPasswordRepeat.Location = New System.Drawing.Point(441, 58)
    Me.txtOldPasswordRepeat.Name = "txtOldPasswordRepeat"
    Me.txtOldPasswordRepeat.Size = New System.Drawing.Size(187, 20)
    Me.txtOldPasswordRepeat.TabIndex = 4
    Me.txtOldPasswordRepeat.UseSystemPasswordChar = True
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(18, 253)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(90, 13)
    Me.Label3.TabIndex = 19
    Me.Label3.Text = "Update Activities:"
    '
    'chkUpdateObjects
    '
    Me.chkUpdateObjects.CheckOnClick = True
    Me.chkUpdateObjects.FormattingEnabled = True
    Me.chkUpdateObjects.Location = New System.Drawing.Point(18, 269)
    Me.chkUpdateObjects.Name = "chkUpdateObjects"
    Me.chkUpdateObjects.Size = New System.Drawing.Size(610, 94)
    Me.chkUpdateObjects.Sorted = True
    Me.chkUpdateObjects.TabIndex = 7
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(18, 409)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(81, 13)
    Me.Label4.TabIndex = 21
    Me.Label4.Text = "New Password:"
    '
    'txtNewPassword
    '
    Me.txtNewPassword.Location = New System.Drawing.Point(103, 405)
    Me.txtNewPassword.Name = "txtNewPassword"
    Me.txtNewPassword.Size = New System.Drawing.Size(187, 20)
    Me.txtNewPassword.TabIndex = 8
    Me.txtNewPassword.UseSystemPasswordChar = True
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(345, 409)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(94, 13)
    Me.Label5.TabIndex = 22
    Me.Label5.Text = "Confirm Password:"
    '
    'txtNewPasswordRepeat
    '
    Me.txtNewPasswordRepeat.Location = New System.Drawing.Point(441, 405)
    Me.txtNewPasswordRepeat.Name = "txtNewPasswordRepeat"
    Me.txtNewPasswordRepeat.Size = New System.Drawing.Size(187, 20)
    Me.txtNewPasswordRepeat.TabIndex = 9
    Me.txtNewPasswordRepeat.UseSystemPasswordChar = True
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(12, 13)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(99, 17)
    Me.Label6.TabIndex = 11
    Me.Label6.Text = "1. Define User"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(64, 39)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(46, 13)
    Me.Label7.TabIndex = 12
    Me.Label7.Text = "User ID:"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(39, 69)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(71, 13)
    Me.Label8.TabIndex = 14
    Me.Label8.Text = "User Domain:"
    '
    'txtUserID
    '
    Me.txtUserID.Location = New System.Drawing.Point(116, 35)
    Me.txtUserID.Name = "txtUserID"
    Me.txtUserID.Size = New System.Drawing.Size(187, 20)
    Me.txtUserID.TabIndex = 0
    '
    'txtDomain
    '
    Me.txtDomain.Location = New System.Drawing.Point(116, 65)
    Me.txtDomain.Name = "txtDomain"
    Me.txtDomain.Size = New System.Drawing.Size(119, 20)
    Me.txtDomain.TabIndex = 1
    '
    'chkUserIDFormats
    '
    Me.chkUserIDFormats.BackColor = System.Drawing.SystemColors.Control
    Me.chkUserIDFormats.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.chkUserIDFormats.CheckOnClick = True
    Me.chkUserIDFormats.FormattingEnabled = True
    Me.chkUserIDFormats.Items.AddRange(New Object() {"Domain\UserID", "UserID", "UserID@Domain"})
    Me.chkUserIDFormats.Location = New System.Drawing.Point(116, 95)
    Me.chkUserIDFormats.Name = "chkUserIDFormats"
    Me.chkUserIDFormats.Size = New System.Drawing.Size(174, 45)
    Me.chkUserIDFormats.TabIndex = 2
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(24, 111)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(86, 13)
    Me.Label9.TabIndex = 16
    Me.Label9.Text = "User ID Formats:"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(12, 177)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(117, 17)
    Me.Label10.TabIndex = 18
    Me.Label10.Text = "2. Define Objects"
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(12, 383)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(136, 17)
    Me.Label11.TabIndex = 20
    Me.Label11.Text = "3. Define New Value"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label12.Location = New System.Drawing.Point(344, 85)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(284, 13)
    Me.Label12.TabIndex = 17
    Me.Label12.Text = "Only needed if Variables/Configurations should be updated"
    '
    'lnkSelectAll
    '
    Me.lnkSelectAll.AutoSize = True
    Me.lnkSelectAll.Location = New System.Drawing.Point(531, 366)
    Me.lnkSelectAll.Name = "lnkSelectAll"
    Me.lnkSelectAll.Size = New System.Drawing.Size(18, 13)
    Me.lnkSelectAll.TabIndex = 23
    Me.lnkSelectAll.TabStop = True
    Me.lnkSelectAll.Text = "All"
    '
    'lnkSelectNone
    '
    Me.lnkSelectNone.AutoSize = True
    Me.lnkSelectNone.Location = New System.Drawing.Point(555, 366)
    Me.lnkSelectNone.Name = "lnkSelectNone"
    Me.lnkSelectNone.Size = New System.Drawing.Size(33, 13)
    Me.lnkSelectNone.TabIndex = 24
    Me.lnkSelectNone.TabStop = True
    Me.lnkSelectNone.Text = "None"
    '
    'lnkSelectInvert
    '
    Me.lnkSelectInvert.AutoSize = True
    Me.lnkSelectInvert.Location = New System.Drawing.Point(594, 366)
    Me.lnkSelectInvert.Name = "lnkSelectInvert"
    Me.lnkSelectInvert.Size = New System.Drawing.Size(34, 13)
    Me.lnkSelectInvert.TabIndex = 25
    Me.lnkSelectInvert.TabStop = True
    Me.lnkSelectInvert.Text = "Invert"
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Location = New System.Drawing.Point(485, 366)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(40, 13)
    Me.Label13.TabIndex = 26
    Me.Label13.Text = "Select:"
    '
    'workerPasswordChanger
    '
    '
    'Label14
    '
    Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label14.Location = New System.Drawing.Point(334, 195)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(294, 48)
    Me.Label14.TabIndex = 27
    Me.Label14.Text = "Variables and Configurations update depends on the Old Password since it works as" & _
    " a ""Search and Replace"": it finds the old password and replaces it by the new on" & _
    "e."
    Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(297, 203)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 28
    Me.PictureBox1.TabStop = False
    '
    'dlgChangeUserPassword
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(643, 487)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.Label14)
    Me.Controls.Add(Me.Label13)
    Me.Controls.Add(Me.lnkSelectInvert)
    Me.Controls.Add(Me.lnkSelectNone)
    Me.Controls.Add(Me.lnkSelectAll)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.Label11)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.chkUserIDFormats)
    Me.Controls.Add(Me.txtDomain)
    Me.Controls.Add(Me.txtUserID)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.txtNewPasswordRepeat)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.txtNewPassword)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.chkUpdateObjects)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtOldPasswordRepeat)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtOldPassword)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.chkUpdateConfigurations)
    Me.Controls.Add(Me.chkUpdateVariables)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgChangeUserPassword"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Change Password on Objects"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents chkUpdateVariables As System.Windows.Forms.CheckBox
  Friend WithEvents chkUpdateConfigurations As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtOldPassword As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtOldPasswordRepeat As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkUpdateObjects As System.Windows.Forms.CheckedListBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents txtNewPasswordRepeat As System.Windows.Forms.TextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents txtUserID As System.Windows.Forms.TextBox
  Friend WithEvents txtDomain As System.Windows.Forms.TextBox
  Friend WithEvents chkUserIDFormats As System.Windows.Forms.CheckedListBox
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lnkSelectAll As System.Windows.Forms.LinkLabel
  Friend WithEvents lnkSelectNone As System.Windows.Forms.LinkLabel
  Friend WithEvents lnkSelectInvert As System.Windows.Forms.LinkLabel
  Friend WithEvents Label13 As System.Windows.Forms.Label
  Friend WithEvents workerPasswordChanger As System.ComponentModel.BackgroundWorker
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Simulate_Button As System.Windows.Forms.Button

End Class
