from sqlalchemy import Column, Integer, String, ForeignKey
from app.database.connection import Base

class image(Base):
    id = Column(Integer, primary_key=True)
    page_id = Column(Integer, ForeignKey)
    imageUrl = Column(String)
    image_order = Column(Integer)

    # RELACOES