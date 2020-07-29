package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"os"
	"plugin"
)

func readInputFromJsonFile(path string) (map[string]interface{}, error) {
	json_map := make(map[string]interface{})
	f, err := os.Open(path)
	if err != nil {
		return nil, err
	}
	bytes, err := ioutil.ReadAll(f)
	if err != nil {
		return nil, err
	}
	err = json.Unmarshal(bytes, &json_map)
	if err != nil {
		return nil, err
	}
	return json_map, nil
}
func writeOutputToJsonFile(path string) {

}

func main() {

	module := "test.so"        // os.Args[1]
	function := "Hello"        // os.Args[2]
	input_file := "input.json" //os.Args[3]
	// output_file := "output.json"

	json_map, err := readInputFromJsonFile(input_file)
	fmt.Println(json_map)
	p, err := plugin.Open(module)
	if err != nil {
		panic(err)
	}
	f, err := p.Lookup(function)
	if err != nil {
		panic(err)
	}
	f.(func())()
	//writeOutputToJsonFile(output_file, res)
}
