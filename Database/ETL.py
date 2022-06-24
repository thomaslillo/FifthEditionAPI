# convert original JSON data to CSV for upload 
import pandas as pd
import glob
import json
from os.path import join
import requests
import config
import sqlalchemy
from sqlalchemy.exc import SQLAlchemyError
import re


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

     def spells_extract(self):
          print("extract running...")
          # for eventual collection
          spells_url = 'https://github.com/5e-bits/5e-database/blob/main/src/5e-SRD-Races.json'

          # FOR NOW == just read from the json files not from the web

          # just reading in the spells path
          spells_path = join('Database','Data','5e-SDR-Spells.json')

          with open(spells_path, 'r') as j:
               contents = json.loads(j.read())

          self.spells_df = pd.DataFrame.from_dict(pd.json_normalize(contents))

          print("extract complete...")

     def spells_transform(self):
          print('transform running...')

          # just take out the index and create a list
          def get_classes(classCol):
               #[{'index': 'sorcerer', 'name': 'Sorcerer', 'url': '/api/classes/sorcerer'}, {'index': 'wizard', 'name': 'Wizard', 'url': '/api/classes/wizard'}]
               return re.findall("ex\':\s\'([a-z]+)\'", classCol)

          def clean_strings(strCol):
               # trim the first 2 and last 2 characters
               return strCol[2:-2]

          # clean json 
          self.spells_df['classes'] = self.spells_df['classes'].astype(str).apply(get_classes)
          # remove excess characters from strings
          self.spells_df['desc'] = self.spells_df['desc'].astype(str).apply(clean_strings)
          self.spells_df['higher_level'] = self.spells_df['higher_level'].astype(str).apply(clean_strings)

          # only take required columns
          self.transformed_spellsdf = self.spells_df[['index','name','desc','higher_level','range','components','material','ritual',
                                                  'duration','concentration','casting_time','level','attack_type','classes',
                                                  'damage.damage_type.name','damage.damage_at_slot_level.1','damage.damage_at_slot_level.2',
                                                  'damage.damage_at_slot_level.3','damage.damage_at_slot_level.4','damage.damage_at_slot_level.5',
                                                  'damage.damage_at_slot_level.6','damage.damage_at_slot_level.7','damage.damage_at_slot_level.8',
                                                  'damage.damage_at_slot_level.9','damage.damage_at_character_level.1','damage.damage_at_character_level.5',
                                                  'damage.damage_at_character_level.11','damage.damage_at_character_level.17','heal_at_slot_level.1',
                                                  'heal_at_slot_level.2','heal_at_slot_level.3','heal_at_slot_level.4','heal_at_slot_level.5',
                                                  'heal_at_slot_level.6','heal_at_slot_level.7','heal_at_slot_level.8','heal_at_slot_level.9',
                                                  'area_of_effect.type','area_of_effect.size','school.name','dc.dc_type.index','dc.dc_type.name',
                                                  'dc.dc_success','dc.desc']]     
          print("load complete...")

     def spells_load(self):
          print("load running...")
          
          # write to a csv to check things
          self.transformed_spellsdf.to_csv(join('Database','Data',"Spells.csv"), index=False)

          # write to sql server
          try:
               self.transformed_spellsdf.to_sql('SDR_Spells',self.sql_engine,index=False,if_exists='append',schema='dbo')
          except SQLAlchemyError as e:
               error = str(e.__dict__['orig'])
               return error

          print("load complete...")

if __name__ == "__main__":
     
     # create an instance of DNDETL
     ETL_engine = DNDETL()

     # run the process
     ETL_engine.spells_extract()
     ETL_engine.spells_transform()
     ETL_engine.spells_load()