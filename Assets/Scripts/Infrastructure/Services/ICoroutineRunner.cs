using System.Collections;
using UnityEngine;

namespace Infrastructure.Services
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}