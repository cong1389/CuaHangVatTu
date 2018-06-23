Imports System
Imports System.Data

Partial Public Class Common_CtrListPage : Inherits Web.UI.UserControl

    Public Delegate Sub ListPagerEventHandler(ByVal sender As Object, ByVal eventArgument As Integer)
    Public Delegate Sub NextPagerEventHandler(ByVal sender As Object, ByVal eventArgument As Integer)
    Public Event PageIndexNextEvent As NextPagerEventHandler

    Private _numSize As Integer = PageNumber.NumberSize
    Private _pageSize As Integer = PageNumber.PageSize

#Region "Properties"

    Public Property NumSize() As Integer
        Get
            Return _numSize
        End Get
        Set(ByVal value As Integer)
            _numSize = value
        End Set
    End Property

    Public Property CurrentPageIndex() As Integer
        Get
            If ViewState("CurrentPageIndex") Is Nothing Then
                Dim page As Integer = 0
                If Request("page") IsNot Nothing Then
                    Integer.TryParse(Request("page"), page)
                    page = page - 1
                End If

                ' set PagerCurrentIndex
                If page > (PagerCurrentPageIndex + 1) * PagerPageSize Then
                    PagerCurrentPageIndex = page \ PageSize - 1
                End If
                ViewState("CurrentPageIndex") = page
            End If

            Return CInt(ViewState("CurrentPageIndex"))
        End Get
        Set(ByVal value As Integer)
            ViewState("CurrentPageIndex") = value
        End Set
    End Property

    Public Property PageCount() As Integer
        Get
            If ViewState("PageCount") Is Nothing Then
                ViewState("PageCount") = 1
            End If

            Return CInt(ViewState("PageCount"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PageCount") = value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            If ViewState("PageSize") Is Nothing Then
                Dim size As Integer = 0
                If Request("size") IsNot Nothing Then
                    Integer.TryParse(Request("size"), size)
                End If
                'ListItem item = NumberOfItemOnPageDropDownList.Items.FindByValue(size.ToString());
                'if (item != null)
                '{
                '    NumberOfItemOnPageDropDownList.SelectedValue = size.ToString();
                '}
                'Convert.ToInt32(NumberOfItemOnPageDropDownList.SelectedValue);
                ViewState("PageSize") = NumSize
            End If

            Return CInt(ViewState("PageSize"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PageSize") = value
        End Set
    End Property

    Protected Property PagerCurrentPageIndex() As Integer
        Get
            If ViewState("PagerCurrentPageIndex") Is Nothing Then
                ViewState("PagerCurrentPageIndex") = 0
            End If

            Return CInt(ViewState("PagerCurrentPageIndex"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PagerCurrentPageIndex") = value
        End Set
    End Property

    Protected Property PagerPageSize() As Integer
        Get
            If ViewState("PagerPageSize") Is Nothing Then
                ViewState("PagerPageSize") = _pageSize
            End If

            Return CInt(ViewState("PagerPageSize"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PagerPageSize") = value
        End Set
    End Property

    Protected Property PagerPageCount() As Integer
        Get
            If ViewState("PagerPageCount") Is Nothing Then
                ViewState("PagerPageCount") = 0
            End If

            Return CInt(ViewState("PagerPageCount"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PagerPageCount") = value
        End Set
    End Property

    Public Property ScriptManager() As ScriptManager
        Get
            Return m_scriptManager
        End Get
        Set(ByVal value As ScriptManager)
            m_scriptManager = value
        End Set
    End Property

#End Region

    Protected m_scriptManager As ScriptManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        NextPager.ToolTip = "Next"
        PreviousPager.ToolTip = "Previous"
        ' add postback controls
        If ScriptManager IsNot Nothing Then
            ScriptManager.RegisterPostBackControl(PageList)
        End If

    End Sub

    ''' <summary>
    ''' Refresh pager data
    ''' </summary>
    Dim TotalPageSize As Integer
    Public Sub RefreshPager()
        If CurrentPageIndex = PageCount Then
            CurrentPageIndex = PageCount - 1
        Else
            If CurrentPageIndex > PageCount Then
                CurrentPageIndex = 0
            End If
        End If

        ' bind data for Pager
        PagerPageCount = If(PageCount Mod PagerPageSize = 0, PageCount \ PagerPageSize, PageCount \ PagerPageSize + 1)

        Dim listPages As New SortedList()
        For i As Integer = PagerCurrentPageIndex * PagerPageSize To (PagerCurrentPageIndex + 1) * PagerPageSize - 1
            If i < PageCount Then
                listPages.Add(i, i + 1)
            End If
        Next
        TotalPageSize = listPages.Count
        PageList.DataSource = listPages
        PageList.DataBind()
      
        ' show hide buttons
        'NextPagerContainer.Visible = PagerCurrentPageIndex == PagerPageCount - 1 ? false : true;
        'PreviousPagerContainer.Visible = PagerCurrentPageIndex == 0 ? false : true;
        PagerPanel.Visible = If(listPages.Count = 1, False, True)
        PageList.Visible = If(listPages.Count = 1, False, True)

        'NextPagerContainer.Disabled = CurrentPageIndex == PageCount - 1 ? false : true;
        If CurrentPageIndex = PageCount - 1 Then
            NextPagerContainer.Attributes.Add("class", "disabled")
            NextPager.Enabled = False
        Else
            NextPagerContainer.Attributes.Remove("class")
            NextPager.Enabled = True
        End If
        'LastLinkContainer.Visible = CurrentPageIndex == PageCount - 1 ? false : true;
        'CurrentPageIndex == 0 ? PreviousPagerContainer.Attributes.Remove("class") : PreviousPagerContainer.Attributes.Add("class", "disabled");
        If CurrentPageIndex = 0 Then
            PreviousPagerContainer.Attributes.Add("class", "disabled")
            PreviousPager.Enabled = False
        Else
            PreviousPagerContainer.Attributes.Remove("class")
            PreviousPager.Enabled = True
        End If
        If PagerPageCount = 0 Then
            PagerPanel.Visible = False
        End If
        'FirstLinkContainer.Visible = CurrentPageIndex == 0 ? false : true;
    End Sub

    ''' <summary>
    ''' Raise event
    ''' </summary>
    Private Sub [RaiseEvent]()

        ' raise next pag
        RaiseEvent PageIndexNextEvent(Me, CurrentPageIndex)

    End Sub

    Protected Sub PageList_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        ' change current page
        CurrentPageIndex = Convert.ToInt32(e.CommandName)
        [RaiseEvent]()
    End Sub

    Protected Sub PreviousLink_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' change current page
        CurrentPageIndex = CurrentPageIndex - 1
        If CurrentPageIndex < PagerCurrentPageIndex * PagerPageSize Then
            PagerCurrentPageIndex -= 1
        End If
        [RaiseEvent]()
    End Sub

    Protected Sub NextLink_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' change current page
        CurrentPageIndex = CurrentPageIndex + 1
        If CurrentPageIndex >= (PagerCurrentPageIndex + 1) * PagerPageSize Then
            PagerCurrentPageIndex += 1
        End If
        [RaiseEvent]()
    End Sub
    Dim countRow As Integer = 1
    Protected Sub PageList_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' set CSS for current page

            Dim PageIndexLink As LinkButton = TryCast(e.Item.FindControl("PageIndexLink"), LinkButton)
            Dim Item As HtmlGenericControl = TryCast(e.Item.FindControl("Item"), HtmlGenericControl)

            If CurrentPageIndex >= 5 Then
                If countRow <= CurrentPageIndex - 4 Or countRow > CurrentPageIndex + 6 Then
                    Item.Visible = False
                    PageIndexLink.Visible = False
                End If

            Else
                If countRow > 11 Then
                    Item.Visible = False
                    PageIndexLink.Visible = False
                End If
            End If


            'Item.Visible = False
            'Dim drv As DataRowView = e.Item.DataItem
            PageIndexLink.ToolTip = String.Format("{0} {1}", "Page", Convert.ToInt32(PageIndexLink.CommandName) + 1)
            'PageIndexLink.Text = drv.Item("Value")
            If PageIndexLink.CommandName.Equals(CurrentPageIndex.ToString()) Then
                PageIndexLink.Enabled = False
                Item.Attributes.Add("class", "current")

                PageIndexLink.Enabled = False

            End If
            countRow = countRow + 1
        End If
    End Sub

    Protected Sub PreviousPager_Click(ByVal sender As Object, ByVal e As EventArgs)

        CurrentPageIndex -= 1
        [RaiseEvent]()
    End Sub

    Protected Sub NextPager_Click(ByVal sender As Object, ByVal e As EventArgs)
        CurrentPageIndex += 1
        [RaiseEvent]()
    End Sub

    Protected Sub lbtnChangePageSize_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' get selected index
        Dim index As Integer = Convert.ToInt32(Request.Params.[Get]("__EVENTARGUMENT"))
        'NumberOfItemOnPageDropDownList.SelectedIndex = index;

        ' change page size
        PageSize = NumSize
        ' Convert.ToInt32(NumberOfItemOnPageDropDownList.SelectedValue);
        ' reset current page index
        CurrentPageIndex = 0
        ' reset current pager index
        PagerCurrentPageIndex = 0
        'NumberOfItemOnPageDropDownList.ToolTip = string.Format("Show {0} items on page", NumberOfItemOnPageDropDownList.SelectedValue);
        [RaiseEvent]()
    End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================

End Class
