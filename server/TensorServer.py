from flask import Flask, request, jsonify
import os
#import numpy as np

# configuration
file_extension = 'uvw'
data_directory_path = './data'

def scan_datasets(data_dir):
    # scan all files and subdirectories for 
    tensors = []
    with os.scandir(data_dir) as filelist:
        for file in filelist:
            if file.is_file() and file.name.endswith(file_extension):
                file_meta = {'name': file.name, 'size': file.stat().st_size}
                with open(file, "rb") as f:
                    b = f.read(3)
                    s0 = str(b[0]); s1 = str(b[1]); s2 = str(b[2])
                    file_meta.update({'shape': '('+s0+', '+s1+', '+s2+', 3)'})
                tensors.append(file_meta)
    return(tensors)

def load_tensor(name, data_dir):
    with os.scandir(data_dir) as filelist:
        for file in filelist:
            if file.name == name and file.name.endswith(file_extension):
                with open(file, "rb") as f:
                    b = f.read(3)
                    s0 = int(b[0]); s1 = int(b[1]); s2 = int(b[2])
                    tensor_values = f.read(s0*s1*s2*3)
    return(tensor_values)

app = Flask(__name__)

@app.route('/', methods=['GET'])
def index():
    
    welcome_string = "Tensor Server - WELCOME"
    return welcome_string

@app.route('/tensor', methods=['GET'])
def get_tensors():
    
    tensors = scan_datasets(data_directory_path)
                    
    name = request.args.get('name', None)
    if name == None:
        # return meta dict with all UVW databases
        return jsonify(tensors)
    
    else:
        # return RGB24 tuple values of specified tensor as byte stream
        return load_tensor(name, data_directory_path)
    
if __name__ == '__main__':
    app.run(debug = True)