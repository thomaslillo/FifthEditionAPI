# convert original JSON data to CSV for upload 
import pandas as pd
import glob
import json
from os.path import join
import requests
import config
import sqlalchemy


class DNDETL:

     def __init__(self):
          print('ETL starting...')
          try:
               self.sql_engine = sqlalchemy.create_engine('mssql://*server_name*/*database_name*?trusted_connection=yes')
          except:
               print("could not create sql engine")

     def extract(self):
          print("extract running...")
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

          self.spells_df = pd.DataFrame.from_dict(pd.json_normalize(contents))

          print("extract successful...")

     def transform(self):
          print('transform')

     def load(self):
          print("load running...")
          
          # write to a csv to check things
          self.spells_df.to_csv(join('Database','Data',"Spells.csv"), index=False)



          print("load successful...")

if __name__ == "__main__":
     
     # create an instance of DNDETL
     ETL_engine = DNDETL()

     # run the extract process
     ETL_engine.extract()

     # run the load processes
     ETL_engine.load()

