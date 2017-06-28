Imports System.Windows.Forms

Public Class dlgSearching
  Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      Const CS_DBLCLKS As Int32 = &H8
      Const CS_NOCLOSE As Int32 = &H200
      cp.ClassStyle = CS_DBLCLKS Or CS_NOCLOSE
      Return cp
    End Get
  End Property
End Class
