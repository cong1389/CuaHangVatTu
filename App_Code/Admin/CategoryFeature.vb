Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class CategoryFeature
#Region "Khai báo"
        Private mCategoryNo_ As String = ""
        Private mFeatureGroupNo_ As String = ""
        Private mFeatureNo_ As String = ""
        Private mName As String = ""
        Private mDescription As String = ""
        Private mPositionOrder As Integer = 0
        Private mLinkTable As Integer = 0
        Private mOptionString As String = ""
        Private mLastDateModify As String = ""
        Private mUserID As String = ""
        Private mShowInList As Integer = 0
        Private mUnitOfMeasure As String = ""
        Private mFilter As Integer = 0
        Property CategoryNo_() As String
            Get
                Return mCategoryNo_
            End Get
            Set(ByVal value As String)
                mCategoryNo_ = value
            End Set
        End Property
        Property FeatureGroupNo_() As String
            Get
                Return mFeatureGroupNo_
            End Get
            Set(ByVal value As String)
                mFeatureGroupNo_ = value
            End Set
        End Property
        Property FeatureNo_() As String
            Get
                Return mFeatureNo_
            End Get
            Set(ByVal value As String)
                mFeatureNo_ = value
            End Set
        End Property
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
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
        Property PositionOrder() As Integer
            Get
                Return mPositionOrder
            End Get
            Set(ByVal value As Integer)
                mPositionOrder = value
            End Set
        End Property
        Property LinkTable() As Integer
            Get
                Return mLinkTable
            End Get
            Set(ByVal value As Integer)
                mLinkTable = value
            End Set
        End Property
        Property OptionString() As String
            Get
                Return mOptionString
            End Get
            Set(ByVal value As String)
                mOptionString = value
            End Set
        End Property
        Property LastDateModify() As String
            Get
                Return mLastDateModify
            End Get
            Set(ByVal value As String)
                mLastDateModify = value
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
        Property ShowInList As Integer
            Get
                Return mShowInList
            End Get
            Set(ByVal value As Integer)
                mShowInList = value
            End Set
        End Property
        Property UnitOrMeasure As String
            Get
                Return mUnitOfMeasure
            End Get
            Set(ByVal value As String)
                mUnitOfMeasure = value
            End Set
        End Property
        Property Filter As Integer
            Get
                Return mFilter
            End Get
            Set(ByVal value As Integer)
                mFilter = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sCategoryNo_ As String, ByVal sFeatureGroupNo_ As String, ByVal sFeatureNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = "  SELECT * FROM [Categoy Feature] WHERE [Category No_] = '" & sCategoryNo_.Trim & "' and [Feature Group No_] = '" & sFeatureGroupNo_.Trim & "' and [Feature No_] = '" & sFeatureNo_.Trim & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mCategoryNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mFeatureGroupNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mFeatureNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDescription = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPositionOrder = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mLinkTable = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mOptionString = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mLastDateModify = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mShowInList = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mUnitOfMeasure = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mFilter = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
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
        Public Function Save(ByVal sCategoryNoOld As String, ByVal sFeatureGroupNoOld As String, ByVal sFeatureNoOld As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(15) {}
            Try
                arParams(0) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(1) = DBHelper.createParameter("@FeatureGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mFeatureGroupNo_)
                arParams(2) = DBHelper.createParameter("@FeatureNo_", SqlDbType.NVarChar, ParameterDirection.Input, mFeatureNo_)
                arParams(3) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(4) = DBHelper.createParameter("@Description", SqlDbType.NVarChar, ParameterDirection.Input, mDescription)
                arParams(5) = DBHelper.createParameter("@PositionOrder", SqlDbType.Int, ParameterDirection.Input, mPositionOrder)
                arParams(6) = DBHelper.createParameter("@LinkTable", SqlDbType.Int, ParameterDirection.Input, mLinkTable)
                arParams(7) = DBHelper.createParameter("@OptionString", SqlDbType.NVarChar, ParameterDirection.Input, mOptionString)
                arParams(8) = DBHelper.createParameter("@LastDateModify", SqlDbType.NVarChar, ParameterDirection.Input, mLastDateModify)
                arParams(9) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(10) = DBHelper.createParameter("@ShowInList", SqlDbType.Int, ParameterDirection.Input, mShowInList)
                arParams(11) = DBHelper.createParameter("@UnitOfMeasure", SqlDbType.NVarChar, ParameterDirection.Input, mUnitOfMeasure)
                arParams(12) = DBHelper.createParameter("@Filter", SqlDbType.Int, ParameterDirection.Input, mFilter)
                arParams(13) = DBHelper.createParameter("@CategoryNo_Old", SqlDbType.NVarChar, ParameterDirection.Input, sCategoryNoOld)
                arParams(14) = DBHelper.createParameter("@FeatureGroupNo_Old", SqlDbType.NVarChar, ParameterDirection.Input, sFeatureGroupNoOld)
                arParams(15) = DBHelper.createParameter("@FeatureNo_Old", SqlDbType.NVarChar, ParameterDirection.Input, sFeatureNoOld)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CategoyFeature", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Update(ByVal sCategoryNoOld As String, ByVal sFeatureGroupNoOld As String, ByVal sFeatureNoOld As String) As Boolean
            Try
                Dim SQL As String
                SQL = " DELETE FROM [Categoy Feature] WHERE [Category No_] = '" & sCategoryNoOld & "' AND [Feature Group No_] = '" & sFeatureGroupNoOld & "' AND [Feature No_] = '" & sFeatureNoOld & "' "
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)

                SQL = " UPDATE [Categoy Feature] SET [Position Order] = [Position Order] + 1 WHERE [Category No_] = '" & mCategoryNo_ & "' AND [Position Order] >= " & mPositionOrder
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)

                SQL = " INSERT INTO [Categoy Feature] ([Category No_],[Feature Group No_],[Feature No_],[Name],[Description],[Position Order]," & _
                            "[Link Table],[Option String],[Last Date Modify],[User ID], [Show In List], [Unit Of Measure])" & _
                        " VALUES ('" & mCategoryNo_ & "','" & mFeatureGroupNo_ & "','" & mFeatureNo_ & "',N'" & mName & "',N'" & mDescription & _
                                "'," & mPositionOrder & ",'" & mLinkTable & "',N'" & mOptionString & "','" & mLastDateModify & "','" & mUserID & "'," & mShowInList & ",'" & mUnitOfMeasure & "')"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace
