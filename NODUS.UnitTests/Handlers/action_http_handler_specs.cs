using Machine.Specifications;
using NODUS;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Handlers;
using NODUS.Utilities;
using It = Machine.Specifications.It;

namespace Nodus.UnitTests.Handlers {

	[ Subject( typeof( ActionHttpHandler ) ) ]
	public class action_http_handler_specs : NodusInitialContext {
		
		public class execute_action {
			static MockedHttpContext mock = new MockedHttpContext();
			static ActionHttpHandler action_handler;

			Establish c = ( ) => {
				var action = Create.SingletonOf<node_action>( );
				mock.set_relative_path( "~/node/action/" + action.Id );

				action_handler = new ActionHttpHandler( mock.request_context );
			};

			Because of = ( ) => action_handler.ProcessRequest( mock.http_context );

			It should_reply_sucessfully_with_the_expected_data = ( ) => {
				var expected = new Reply( ).sucessfully( ).with( "node_action executed!" );

				mock.string_builder.ToString( ).ShouldEqual( expected.to_json( ) );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}

		public class execute_action_with_arguments {
			static MockedHttpContext mock = CustomHttpContextProvider.GetMock();
			static ActionHttpHandler action_handler;
			
			Establish c = ( ) => {
				var action = Create.SingletonOf<node_action_with_argument>( );
				var encoded_query = "name=test".encode_to_base64( );
				
				mock.set_relative_path( "~/node/action/" + action.Id );
				mock.set_request_indexed_data( "query", encoded_query );

				action_handler = new ActionHttpHandler( mock.request_context );
			};

			Because of = ( ) => {
				action_handler.ProcessRequest( mock.http_context );
			};

			It should_reply_sucessfully_with_the_expected_data = ( ) => {
				var argument = new node_action_argument { name = "test" };
				var expected = new Reply( ).sucessfully( ).with( argument );
				
				mock.string_builder.ToString( ).ShouldEqual( expected.to_json( ) );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}

		public class execute_action_with_passing_validation {
			static readonly MockedHttpContext mock = CustomHttpContextProvider.GetMock();
			static ActionHttpHandler action_handler;
			
			Establish c = ( ) => {
				var action = Create.SingletonOf<node_action_with_validation>( );
				var encoded_query = "name=test".encode_to_base64( );
				
				mock.set_relative_path( "~/node/action/" + action.Id );
				mock.set_request_indexed_data( "query", encoded_query );

				action_handler = new ActionHttpHandler( mock.request_context );
			};

			Because of = ( ) => {
				action_handler.ProcessRequest( mock.http_context );
			};

			It should_reply_sucessfully_with_the_expected_data = ( ) => {
				var argument = new node_action_argument { name = "test" };
				var expected = new Reply( ).sucessfully( ).with( argument );
				
				mock.string_builder.ToString( ).ShouldEqual( expected.to_json( ) );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}

		public class execute_action_with_failing_validation {
			static readonly MockedHttpContext mock = CustomHttpContextProvider.GetMock();
			static ActionHttpHandler action_handler;
			
			Establish c = ( ) => {
				var action = Create.SingletonOf<node_action_with_validation>( );
				var encoded_query = "name=".encode_to_base64( );
				
				mock.set_relative_path( "~/node/action/" + action.Id );
				mock.set_request_indexed_data( "query", encoded_query );

				action_handler = new ActionHttpHandler( mock.request_context );
			};

			Because of = ( ) => {
				action_handler.ProcessRequest( mock.http_context );
			};

			It should_reply_sucessfully_with_the_expected_data = ( ) => {
				var exception = new ReplyException(1, "Name is required.");
				var expected = new Reply( ).failed( ).with( exception );
				
				mock.string_builder.ToString( ).ShouldEqual( expected.to_json( ) );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}
	}

}