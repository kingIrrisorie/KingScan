from sqlalchemy import Column, Integer, String, ForeignKey
from sqlalchemy.orm import relationship
from app.database.connection import Base


class Image(Base):
    __tablename__ = "image"

    id = Column(Integer, primary_key=True)
    page_id = Column(Integer, ForeignKey("page.id"))
    image_url = Column(String)
    image_order = Column(Integer)

    # RELAÇÕES
    page = relationship(
        "Page",
        back_populates="images"
    )