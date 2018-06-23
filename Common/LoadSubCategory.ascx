<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoadSubCategory.ascx.vb"
    Inherits="Common_LoadSubCategory" %>

<!--LoadSubCategory-->
<div class="container bc-category-floor bc-layout-r-main bc-category-floor-title"
    style="display: block;">
    <div class=" util-clearfix">
        <h2 class="bc-title bc-nowrap-ellipsis" title="Dresses">{0}</h2>

    </div>
    <div class="container ">
        <div class="row">
            <div class="column col-xs-12 col-sm-3" id="left_column">
                <div class="bc-hot-tags">
                    <h4 class="bc-title"></h4>
                    <ul class="bc-hot-tag-list bc-tags bc-ul util-clearfix">
                        {1}                            
                    </ul>
                </div>

            </div>

            <div class="center_column col-xs-12 col-sm-9 product-col noPM">

                <ul class="product-list grid filter">
                    {2} 
                </ul>
            </div>
        </div>


    </div>
</div>
