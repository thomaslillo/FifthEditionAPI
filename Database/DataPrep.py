# convert original JSON data to CSV for upload 
import pandas as pd
import glob
import json
from os.path import join
import requests
import config

# all the urls
json_urls = config.config['urls']

# for testing
spells_url = 'https://github.com/5e-bits/5e-database/blob/main/src/5e-SRD-Races.json'

# get all the json files in the folder
# glob finds all the pathnnames matching a specific pattern
# json_files = list(sorted(glob.glob(join("Data", "*.json"))))

# just reading in the spells path
spells_path = join('Database','Data','5e-SDR-Spells.json')

with open(spells_path, 'r') as j:
     contents = json.loads(j.read())

spells_df = pd.DataFrame.from_dict(pd.json_normalize(contents))

print(spells_df.head)
    
spells_df.to_csv(join('Database','Data',"Spells.csv"), index=False)