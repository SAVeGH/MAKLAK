function Tree()
{
}

Tree.Toggle = function (elm,branchId,nodeId) {
	
	var nodeExpander = $(elm);
	var expSelector = "div[data-branchid='" + branchId + "'][data-nodeid='" + nodeId + "']";
	var expContent = $(expSelector);

	expContent.attr('class', 'expandableContentVisible');

	var nestSelector = "div[data-branchid='" + branchId + "'][data-nodeid='" + nodeId + "'][data-nestedcontent]";
	var nestContent = $(nestSelector);

	//var node = nodeExpander.parent().parent();
	var url = 'Search/TreeNode';
	var prm = 'BranchID=' + branchId.toString() + '&NodeID=' + nodeId.toString();
	$.post(url, prm, fillNode);

	function fillNode(data) {
		nestContent.html(data);
	}
}