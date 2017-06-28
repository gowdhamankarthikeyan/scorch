<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPID
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtPID = New System.Windows.Forms.TextBox()
    Me.cmdSearch = New System.Windows.Forms.Button()
    Me.lblRunbookName = New System.Windows.Forms.Label()
    Me.lblServerName = New System.Windows.Forms.Label()
    Me.cmdOpen = New System.Windows.Forms.LinkLabel()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(268, 156)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(83, 29)
    Me.TableLayoutPanel1.TabIndex = 2
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(8, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Close"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(28, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "PID:"
    '
    'txtPID
    '
    Me.txtPID.Location = New System.Drawing.Point(47, 9)
    Me.txtPID.Name = "txtPID"
    Me.txtPID.Size = New System.Drawing.Size(223, 20)
    Me.txtPID.TabIndex = 0
    '
    'cmdSearch
    '
    Me.cmdSearch.Location = New System.Drawing.Point(276, 8)
    Me.cmdSearch.Name = "cmdSearch"
    Me.cmdSearch.Size = New System.Drawing.Size(75, 23)
    Me.cmdSearch.TabIndex = 1
    Me.cmdSearch.Text = "Search"
    Me.cmdSearch.UseVisualStyleBackColor = True
    '
    'lblRunbookName
    '
    Me.lblRunbookName.AutoSize = True
    Me.lblRunbookName.Location = New System.Drawing.Point(13, 49)
    Me.lblRunbookName.Name = "lblRunbookName"
    Me.lblRunbookName.Size = New System.Drawing.Size(57, 13)
    Me.lblRunbookName.TabIndex = 4
    Me.lblRunbookName.Text = "Runbook: "
    '
    'lblServerName
    '
    Me.lblServerName.AutoSize = True
    Me.lblServerName.Location = New System.Drawing.Point(13, 85)
    Me.lblServerName.Name = "lblServerName"
    Me.lblServerName.Size = New System.Drawing.Size(88, 13)
    Me.lblServerName.TabIndex = 5
    Me.lblServerName.Text = "Runbook Server:"
    '
    'cmdOpen
    '
    Me.cmdOpen.AutoSize = True
    Me.cmdOpen.Location = New System.Drawing.Point(13, 121)
    Me.cmdOpen.Name = "cmdOpen"
    Me.cmdOpen.Size = New System.Drawing.Size(33, 13)
    Me.cmdOpen.TabIndex = 6
    Me.cmdOpen.TabStop = True
    Me.cmdOpen.Text = "Open"
    '
    'dlgPID
    '
    Me.AcceptButton = Me.cmdSearch
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(363, 197)
    Me.Controls.Add(Me.cmdOpen)
    Me.Controls.Add(Me.lblServerName)
    Me.Controls.Add(Me.lblRunbookName)
    Me.Controls.Add(Me.cmdSearch)
    Me.Controls.Add(Me.txtPID)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPID"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Search Runbook By PID"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtPID As System.Windows.Forms.TextBox
  Friend WithEvents cmdSearch As System.Windows.Forms.Button
  Friend WithEvents lblRunbookName As System.Windows.Forms.Label
  Friend WithEvents lblServerName As System.Windows.Forms.Label
  Friend WithEvents cmdOpen As System.Windows.Forms.LinkLabel

End Class
