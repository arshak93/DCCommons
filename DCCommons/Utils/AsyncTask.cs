using System;
using UnityEngine;

namespace DCCommons.Utils {
	
	public class AsyncTask : CustomYieldInstruction {

		public delegate void TaskSuccessDelegate();
		public delegate void TaskFailDelegate(Exception error);
		public delegate void TaskCompleteDelegate(Exception error);

		private TaskSuccessDelegate onSuccess;
		private TaskFailDelegate onFail;
		private TaskCompleteDelegate onComplete;
		private bool _keepWaiting = true;

		public AsyncTask OnSuccess(TaskSuccessDelegate onSuccess) {
			this.onSuccess = onSuccess;
			return this;
		}

		public AsyncTask OnFail(TaskFailDelegate onFail) {
			this.onFail = onFail;
			return this;
		}
		
		public AsyncTask OnComplete(TaskCompleteDelegate onComplete) {
			this.onComplete = onComplete;
			return this;
		}

		public void Success() {
			if (onComplete != null) {
				onComplete(null);
			}
			if (onSuccess != null) {
				onSuccess();
			}
			_keepWaiting = false;
		}

		public void Fail(Exception error) {
			if (onComplete != null) {
				onComplete(error);
			}
			if (onFail != null) {
				onFail(error);
			}
			_keepWaiting = false;
		}

		public override bool keepWaiting {
			get { return _keepWaiting; }
		}
	}
	
	public class AsyncTask<T> : CustomYieldInstruction {

		public delegate void TaskSuccessDelegate(T result);
		public delegate void TaskFailDelegate(Exception error);
		public delegate void TaskCompleteDelegate(T result, Exception error);

		private TaskSuccessDelegate onSuccess;
		private TaskFailDelegate onFail;
		private TaskCompleteDelegate onComplete;
		private bool _keepWaiting = true;

		public AsyncTask<T> OnSuccess(TaskSuccessDelegate onSuccess) {
			this.onSuccess = onSuccess;
			return this;
		}

		public AsyncTask<T> OnFail(TaskFailDelegate onFail) {
			this.onFail = onFail;
			return this;
		}
		
		public AsyncTask<T> OnComplete(TaskCompleteDelegate onComplete) {
			this.onComplete = onComplete;
			return this;
		}

		public void Success(T result) {
			if (onComplete != null) {
				onComplete(result, null);
			}
			if (onSuccess != null) {
				onSuccess(result);
			}
			_keepWaiting = false;
		}

		public void Fail(Exception error) {
			if (onComplete != null) {
				onComplete(default(T), error);
			}
			if (onFail != null) {
				onFail(error);
			}
			_keepWaiting = false;
		}
		
		public override bool keepWaiting {
			get { return _keepWaiting; }
		}
	}
}