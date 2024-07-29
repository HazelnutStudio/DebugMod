using MelonLoader;
using UnityEngine;

namespace DebugMod
{
    public class Main : MelonMod
    {
        const float STEP_TIME_AMOUNT = 0.1f;

        private bool _steppingTime = false;
        private float _stepTimer = 0;
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("hey ;]");
            CLog.ModInstance = this;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneLog.LogSceneHierarchy();
            }

            TimeControls();
        }

        private void TimeControls()
        {
            if (_steppingTime)
            {
                _stepTimer += Time.deltaTime;

                if (_stepTimer >= STEP_TIME_AMOUNT)
                {
                    _steppingTime = false;
                    Time.timeScale = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (_steppingTime)
                {
                    return;
                }

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }

                CLog.Info($"Timescale set to {Time.timeScale}");
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    _steppingTime = true;
                }
                else if (_steppingTime == false)
                {
                    Time.timeScale -= 0.1f;
                }

                CLog.Info($"Timescale set to {Time.timeScale}");
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                if (_steppingTime == true)
                {
                    return;
                }

                Time.timeScale += 0.1f;

                CLog.Info($"Timescale set to {Time.timeScale}");
            }
        }
    }
}
