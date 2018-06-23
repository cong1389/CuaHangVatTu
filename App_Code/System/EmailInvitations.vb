Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class EmailInvitations
#Region "Khai báo"
        Private mCustomerNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mFullName As String = ""
        Private mEmail As String = ""
        Private mSend As Integer = 0

        Property CustomerNo_() As String
            Get
                Return mCustomerNo_
            End Get
            Set(ByVal value As String)
                mCustomerNo_ = value
            End Set
        End Property
        Property LineNo_() As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
            End Set
        End Property
        Property FullName() As String
            Get
                Return mFullName
            End Get
            Set(ByVal value As String)
                mFullName = value
            End Set
        End Property
        Property Email() As String
            Get
                Return mEmail
            End Get
            Set(ByVal value As String)
                mEmail = value
            End Set
        End Property
        Property Send() As Integer
            Get
                Return mSend
            End Get
            Set(ByVal value As Integer)
                mSend = value
            End Set
        End Property

#End Region
#Region "Function"
  
        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(4) {}
            Try
                arParams(0) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@FullName", SqlDbType.NVarChar, ParameterDirection.Input, mFullName)
                arParams(3) = DBHelper.createParameter("@Email", SqlDbType.NVarChar, ParameterDirection.Input, mEmail)
                arParams(4) = DBHelper.createParameter("@Send", SqlDbType.Int, ParameterDirection.Input, mSend)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_EmailInvitations", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace