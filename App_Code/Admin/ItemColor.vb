Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class ItemColor

#Region "Khai báo"
        Private mItemOriginNo_ As String = ""
        Private mItemNo_ As String = ""
        Private mDescription As String = ""
        Private mItemColor As String = ""
        Private mLineNo_ As Integer = 0
        Private mPublished As Integer = 0

        Property ItemOriginNo_() As String
            Get
                Return mItemOriginNo_
            End Get
            Set(ByVal value As String)
                mItemOriginNo_ = value
            End Set
        End Property
        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
            End Set
        End Property
        Property Description() As String
            Get
                Return mDescription
            End Get
            Set(ByVal value As String)
                mDescription = value
            End Set
        End Property
        Property ItemColor() As String
            Get
                Return mItemColor
            End Get
            Set(ByVal value As String)
                mItemColor = value
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
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item Color] WHERE [Item No_] = '" & sItemNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemOriginNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mDescription = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mItemColor = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
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
            Dim arParams() As IDataParameter = New IDataParameter(5) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemOriginNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemOriginNo_)
                arParams(1) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(2) = DBHelper.createParameter("@Description", SqlDbType.NVarChar, ParameterDirection.Input, mDescription)
                arParams(3) = DBHelper.createParameter("@ItemColor", SqlDbType.NVarChar, ParameterDirection.Input, mItemColor)
                arParams(4) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(5) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ItemColor", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function AutoLoad(ByVal sItemOriginNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(0) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemOriginNo_", SqlDbType.NVarChar, ParameterDirection.Input, sItemOriginNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_AutoLoadItemColorByModel", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region
    End Class
End Namespace