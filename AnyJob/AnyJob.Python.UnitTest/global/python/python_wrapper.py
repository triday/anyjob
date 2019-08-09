
import sys
import importlib
import traceback
import json

RESULT_SPLIT_TEXT = "***[[[!@#$%^&*()_!@#$%^&*(!@#$%^&]]]***"


def run_action(module_name, run_args):
    module = importlib.import_module(module_name)
    return module.run(**run_args)


if __name__ == "__main__":
    try:
        _, module = sys.argv
        action_argv = json.loads(input())
        action_res = run_action(module, action_argv)
        print(RESULT_SPLIT_TEXT)
        print(json.dumps({'result': action_res}))
    except BaseException as ex:
        error = {
            'type': type(ex).__name__,
            'message': str(ex),
            'stack': traceback.format_exc()
        }
        print(RESULT_SPLIT_TEXT)
        print(json.dumps({'error': error}))
