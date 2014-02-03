using System;

namespace NODUS.Interfaces {

	public interface INodeAction {
		Guid Id { get; }

		string Name { get; }
		string Caption { get; }

		Reply Execute();
		Type ParentType();
	}

}