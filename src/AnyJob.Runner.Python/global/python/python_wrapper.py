import sys
import importlib
import traceback
import json
import io

def run_action(module_name,method_name, run_args):
    module = importlib.import_module(module_name)
    func = getattr(module, method_name)
    return func(**run_args)

def read_input(input_file):
    with io.open(input_file, encoding='utf-8-sig') as f:
        return json.load(f)

def write_output(output_file, result):
    with open(output_file, 'w') as fw:
        json.dump(result,fw)

if __name__ == "__main__":
    _, module, method, input_file, output_file = sys.argv
    try:
        action_argv = read_input(input_file)
        action_res = run_action(module, method, action_argv)
        write_output(output_file,{'result': action_res})
    except BaseException as ex:
        error = {
            'type': type(ex).__name__,
            'message': str(ex),
            'stack': traceback.format_exc()
        }
        write_output(output_file, {'error': error})
