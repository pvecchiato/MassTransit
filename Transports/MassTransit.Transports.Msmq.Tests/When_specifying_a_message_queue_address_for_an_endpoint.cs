// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Transports.Msmq.Tests
{
	using System;
	using Exceptions;
	using MassTransit.Tests;
	using NUnit.Framework;

	[TestFixture]
	public class When_specifying_a_message_queue_address_for_an_endpoint
	{
		[SetUp]
		public void SetUp()
		{
			_address = "msmq://localhost/mt_client";
			_uriAddress = new Uri(_address);
		}

		private string _address;
		private Uri _uriAddress;

		private readonly Uri _expectedUri = new Uri("msmq://" + Environment.MachineName + "/mt_client");
		private const string _expectedQueuePath = @"FormatName:DIRECT=OS:localhost\private$\mt_client";

		[Test]
		public void A_message_queue_address_should_convert_to_a_queue_path()
		{
			MsmqEndpoint endpoint = new MsmqEndpoint(_address);

			endpoint.QueuePath
				.ShouldEqual(_expectedQueuePath);
			endpoint.Uri
				.ShouldEqual(_expectedUri);
		}

		[Test]
		public void A_message_queue_uri_should_convert_to_a_queue_path()
		{
			MsmqEndpoint endpoint = new MsmqEndpoint(_uriAddress);

			endpoint.QueuePath
				.ShouldEqual(_expectedQueuePath);
			endpoint.Uri
				.ShouldEqual(_expectedUri);
		}


		[Test, ExpectedException(typeof (EndpointException))]
		public void An_address_cant_contain_a_path_specifier()
		{
			string address = "msmq://localhost/test_endpoint/error_creator";

			new MsmqEndpoint(address);
		}
	}
}