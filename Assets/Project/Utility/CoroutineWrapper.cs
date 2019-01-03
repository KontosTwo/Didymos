using System;
using System.Collections.Generic;
using System.Collections;
public static class CoroutineWrapper{
    public static IEnumerator CreateCancellable(
        Func<bool> continueCondition,
        Func<bool> cancelCondition,
        Action begin,
        Action action,
        Action cleanup,
        Action onCancel
    ){
        begin();
        while (continueCondition()) {
            action();
            yield return null;
        }
        if (cancelCondition()) {
            onCancel();
        }
        else {
            cleanup();
        }
    }
}

