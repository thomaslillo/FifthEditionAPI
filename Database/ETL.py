# convert original JSON data to CSV for upload 
import pandas as pd
import glob
import json
from os.path import join
import requests
import config
import sqlalchemy
from sqlalchemy.exc import SQLAlchemyError


class DNDETL:

     def __init__(self):
          print('ETL starting...')
          try:
               server = config.config['server_name']
               database = config.config['database_name']
               self.sql_engine = sqlalchemy.create_engine(
                    'mssql+pyodbc://'+server+'/'+database+'?trusted_connection=yes&driver=ODBC+Driver+17+for+SQL+Server',
                    echo=True
               )
          except:
               print("could not create sql engine")

     def extract(self):
          print("extract running...")
          # for testing
          spells_url = 'https://github.com/5e-bits/5e-database/blob/main/src/5e-SRD-Races.json'

          # FOR NOW == just read from the json files not from the web

          # just reading in the spells path
          spells_path = join('Database','Data','5e-SDR-Spells.json')

          with open(spells_path, 'r') as j:
               contents = json.loads(j.read())

          self.spells_df = pd.DataFrame.from_dict(pd.json_normalize(contents))

          print("extract successful...")

     def transform(self):
          print('transform')
          # make data mods later

     def load(self):
          print("load running...")
          
          # write to a csv to check things
          self.spells_df.to_csv(join('Database','Data',"Spells.csv"), index=False)

          try:
               self.spells_df.to_sql('SDR_Spells',self.sql_engine,index=False,if_exists='append',schema='dbo')
          except SQLAlchemyError as e:
               error = str(e.__dict__['orig'])
               return error

          print("load successful...")

if __name__ == "__main__":
     
     # create an instance of DNDETL
     ETL_engine = DNDETL()

     # run the extract process
     ETL_engine.extract()

     # run the load processes
     ETL_engine.load()

