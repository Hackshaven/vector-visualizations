Start the server by first installig Flask and then typing "python TensorServer.py"

Accepts from following API calls
- URL/ -- Simple welcome message. Add Help text in future
- URL/shape -- List the tensor files available, along with info (like shape)
- URL/shape?name=asdf -- Dumps the shape for the 'asdf' tensor in binary
- URL/tensor -- List the tensor files available, along with info (like shape)
- URL/tensor?name=asdf -- Dump the value of the 'asdf' tensor in binary
