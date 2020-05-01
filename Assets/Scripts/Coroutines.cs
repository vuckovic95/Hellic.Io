using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using System;
using UnityEngine.UI;

public class Coroutines : MonoBehaviour
{
    void Awake()
    {
        GlobalManager.Coroutines = this;
    }

    //Do action after some time 
    public IEnumerator WaitForSeconds(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    //Lerp float 
    public IEnumerator LerpFloat(float result, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            float x = Mathf.Lerp(start, end, t);
            result = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        result = end;

        action?.Invoke();
    }

    //Lerp Int
    public IEnumerator LerpInt(int result, int start, int end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            float x = Mathf.Lerp(start, end, t);
            result = (int)x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        result = end;

        action?.Invoke();
    }

    //LerpPositionV3
    public IEnumerator LerpV3Position(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector3 x = Vector3.Lerp(start, end, t);
            tr.position = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        tr.position = end;

        action?.Invoke();
    }

    public IEnumerator LerpV3RB(Rigidbody rb, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector3 x = Vector3.Lerp(start, end, t);
            rb.position = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        rb.position = end;

        action?.Invoke();
    }

    public IEnumerator LerpV3RBZaxis(Rigidbody rb, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            float z = Mathf.Lerp(start, end, t);
            rb.position = new Vector3(rb.position.x, rb.position.y, z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        rb.position = new Vector3(rb.position.x, rb.position.y, end);

        action?.Invoke();
    }

    //LerpPositionV2
    public IEnumerator LerpV2Position(Vector2 result, Vector2 start, Vector2 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector2 x = Vector2.Lerp(start, end, t);
            result = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        result = end;

        action?.Invoke();
    }

    //LerpRotationQuaternion
    public IEnumerator LerpRotation(Transform tr, Quaternion start, Quaternion end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Quaternion x = Quaternion.Lerp(start, end, t);
            tr.rotation = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        tr.rotation = end;

        action?.Invoke();
    }

    //Lerp Slider Value (In-game)
    public IEnumerator LerpSlider(Slider s, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            float x = Mathf.Lerp(start, end, t);
            s.value = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        s.value = end;

        action?.Invoke();
    }

    //Lerp FieldOfView
    public IEnumerator LerpFieldOfView(float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            float f = Mathf.Lerp(start, end, t);
            Camera.main.fieldOfView = f;
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Lerp Scale
    public IEnumerator LerpScale(GameObject object_, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            object_.transform.localScale = new Vector3(x, x, x);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Lerp X axis scale
    public IEnumerator LerpScaleXAxis(GameObject object_, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            object_.transform.localScale = new Vector3(x, object_.transform.localScale.y, object_.transform.localScale.z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Lerp Y axis scale
    public IEnumerator LerpScaleYAxis(GameObject object_, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float y = Mathf.Lerp(start, end, t);
            object_.transform.localScale = new Vector3(object_.transform.localScale.x, y, object_.transform.localScale.z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Lerp Z axis scale
    public IEnumerator LerpScaleZAxis(GameObject object_, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float z = Mathf.Lerp(start, end, t);
            object_.transform.localScale = new Vector3(object_.transform.localScale.x, object_.transform.localScale.y, z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Lerp solid color
    public IEnumerator LerpSolidColor(Color currentValue, Color nextValue, float time, Action action = null)
    {
        float t = 0f;
        while (t < 1f)
        {
            currentValue = Color.Lerp(currentValue, nextValue, t);
            Camera.main.backgroundColor = currentValue;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        currentValue = nextValue;
        action?.Invoke();
    }

    //Timer
    public IEnumerator Timer(float time, Action action = null)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    //Recursive Timer
    public IEnumerator RecursiveTimer(float time, Action action = null)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(RecursiveTimer(time));

        action?.Invoke();
    }

    //Lerp custom axis position. 1 = x axis, 2 = y axis, 3 = z axis, default = lerp all transform
    public IEnumerator LerpPositionWithCustomAxis(Transform tr, Vector3 start, Vector3 end, float time, int custom = 0, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector3 pos = Vector3.Lerp(start, end, t);

            if (custom == 0)
            {
                tr.position = pos;
            }
            else if (custom == 1)
            {
                tr.position = new Vector3(pos.x, tr.position.y, tr.position.z);
            }
            else if (custom == 2)
            {
                tr.position = new Vector3(tr.position.x, pos.y, tr.position.z);
            }
            else if (custom == 3)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y, pos.z);
            }
            else
            {
                Debug.LogError("Wrong parameter entered, 1 - x, 2 - y, 3 - z");
                break;
            }

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        if (custom == 0)
        {
            tr.position = end;
        }
        else if (custom == 1)
        {
            tr.position = new Vector3(end.x, tr.position.y, tr.position.z);
        }
        else if (custom == 2)
        {
            tr.position = new Vector3(tr.position.x, end.y, tr.position.z);
        }
        else if (custom == 3)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, end.z);
        }

        action?.Invoke();
    }
}
