Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class FAQs
#Region "Khai báo"
        Private mLineNo_ As Integer = 0
        Private mQuestion As String = ""
        Private mLink As String = ""
        Private mAnswer As String = ""
        Private mOrderPosition As Integer = 0
        Private mDateCreate As String = ""
        Private mUserID As String = ""
        Private mNumofClick As Integer = 0
        Private mPublished As Integer = 0

        Property LineNo_() As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
            End Set
        End Property
        Property Question() As String
            Get
                Return mQuestion
            End Get
            Set(ByVal value As String)
                mQuestion = value
            End Set
        End Property
        Property Link() As String
            Get
                Return mLink
            End Get
            Set(ByVal value As String)
                mLink = value
            End Set
        End Property
        Property Answer() As String
            Get
                Return mAnswer
            End Get
            Set(ByVal value As String)
                mAnswer = value
            End Set
        End Property
        Property OrderPosition() As Integer
            Get
                Return mOrderPosition
            End Get
            Set(ByVal value As Integer)
                mOrderPosition = value
            End Set
        End Property
        Property DateCreate() As String
            Get
                Return mDateCreate
            End Get
            Set(ByVal value As String)
                mDateCreate = value
            End Set
        End Property
        Property UserID() As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property
        Property NumofClick() As Integer
            Get
                Return mNumofClick
            End Get
            Set(ByVal value As Integer)
                mNumofClick = value
            End Set
        End Property
        Property Published As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal nLineNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [FAQs] WHERE [Line No_] = '" & nLineNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mLineNo_ = GetValue(SqlReader, 0, typeOfColumn.GetInt32)
                    mQuestion = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mLink = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mAnswer = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mOrderPosition = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mDateCreate = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mNumofClick = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    SqlReader.Close()
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try
        End Function
        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(8) {}
            Try
                arParams(0) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(1) = DBHelper.createParameter("@Question", SqlDbType.NVarChar, ParameterDirection.Input, mQuestion)
                arParams(2) = DBHelper.createParameter("@Link", SqlDbType.NVarChar, ParameterDirection.Input, mLink)
                arParams(3) = DBHelper.createParameter("@Answer", SqlDbType.NVarChar, ParameterDirection.Input, mAnswer)
                arParams(4) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                arParams(5) = DBHelper.createParameter("@DateCreate", SqlDbType.NVarChar, ParameterDirection.Input, mDateCreate)
                arParams(6) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(7) = DBHelper.createParameter("@NumofClick", SqlDbType.Int, ParameterDirection.Input, mNumofClick)
                arParams(8) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_FAQs", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace