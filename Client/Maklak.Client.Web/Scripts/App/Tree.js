function Tree()
{
}

Tree.Toggle = function (elm,branchId,nodeId) {
	
	var nodeExpander = $(elm);
	var node = nodeExpander.parent().parent();
	var url = 'Search/TreeNode';
	var prm = 'BranchID=' + branchId.toString() + '&NodeID=' + nodeId.toString();
	$.post(url, prm, fillNode);

	function fillNode(data) {
		node.replaceWith(data);
	}
}