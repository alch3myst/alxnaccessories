package main

import (
	"encoding/base64"
	"flag"
	"os"
)

func main() {

	var fname string
	flag.StringVar(&fname, "name", "TemplateItem", "Acc name")
	flag.Parse()

	csTemplate := "dXNpbmcgVGVycmFyaWE7CnVzaW5nIFRlcnJhcmlhLklEOwp1c2luZyBUZXJyYXJpYS5HYW1lQ29udGVudC5DcmVhdGl2ZTsKdXNpbmcgVGVycmFyaWEuTW9kTG9hZGVyOwoKbmFtZXNwYWNlIGFseG5hY2Nlc3Nvcmllcy5JdGVtcy57c3RhZ2V9CnsKCXB1YmxpYyBjbGFzcyBBcmNoZXJBY2MgOiBNb2RJdGVtIHsKCQlwdWJsaWMgb3ZlcnJpZGUgdm9pZCBTZXRTdGF0aWNEZWZhdWx0cygpCgkJewoJCQlEaXNwbGF5TmFtZS5TZXREZWZhdWx0KCJPbmUgV2l0aCBOYXR1cmUiKTsKCQkJVG9vbHRpcC5TZXREZWZhdWx0KAoJCQkJIkxpbmUxXG4iCgkJCQkrICJMaW5lMlxuIgoJCQkpOwoKCQkJSXRlbS52YWx1ZSA9IEl0ZW0uYnV5UHJpY2UoMCwgMSwgMCwgMCk7CgkJCUl0ZW0ucmFyZSA9IEl0ZW1SYXJpdHlJRC5HcmVlbjsKCgkJCUNyZWF0aXZlSXRlbVNhY3JpZmljZXNDYXRhbG9nLkluc3RhbmNlLlNhY3JpZmljZUNvdW50TmVlZGVkQnlJdGVtSWRbVHlwZV0gPSAxOwoJCX0KCgkJcHVibGljIG92ZXJyaWRlIHZvaWQgU2V0RGVmYXVsdHMoKSB7CgkJCUl0ZW0ud2lkdGggPSA0MDsKCQkJSXRlbS5oZWlnaHQgPSA0MDsKCQkJSXRlbS5hY2Nlc3NvcnkgPSB0cnVlOwoJCX0KCgoJCXB1YmxpYyBvdmVycmlkZSB2b2lkIFVwZGF0ZUFjY2Vzc29yeShQbGF5ZXIgcGxheWVyLCBib29sIGhpZGVWaXN1YWwpIHsJfQoKCgkJcHVibGljIG92ZXJyaWRlIHZvaWQgQWRkUmVjaXBlcygpIHsKCQkJQ3JlYXRlUmVjaXBlKCkKCQkJCS5BZGRJbmdyZWRpZW50KEl0ZW1JRC5CbHVlSmF5KQoJCQkJLkFkZFRpbGUoVGlsZUlELldvcmtCZW5jaGVzKQoJCQkJLlJlZ2lzdGVyKCk7CgkJfQoJfQp9"
	csTemp, _ := base64.StdEncoding.DecodeString(csTemplate)

	imageTemplate := "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAQRJREFUWIXllzEOwyAMRT9RDpOBw3hl4mxMrD5MBm7TLqFyC0khJKRR/4QQ0n8Yg42C0DRND3RQCEHF8SjNtbE9/AHvHhFCdTdfNHuHEIJSOfPZu1NMcz7j56LZOxDRKQDsXQIx9DIHACJKojusrO2mewMwM5j5GgBmhjYW2tgmiOQWlBgD71dKGwtekqs2iasA4q5zivNceZPuk4Rbu5eqzYkigFLzPRD3OYL/BsgVkS3VFLXiCJRC1FbUy4+g6iUkoteTu9ZFnfoUSwPZ3bQ0MtUAEmRvAToEoNU46vIk/C2A1u7mm3JFLckB2d0crVxFvf5rFid6QkRzYImAhOgBIL/nT6g6g5dRv+EgAAAAAElFTkSuQmCC"
	imgTemp, _ := base64.StdEncoding.DecodeString(imageTemplate)

	file, _ := os.Create("../Items/" + fname + ".cs")
	file.WriteString(string(csTemp[:]))
	defer file.Close()

	image, _ := os.Create("../Items/" + fname + ".png")
	image.WriteString(string(imgTemp[:]))
	defer image.Close()
}
